using ElCaptain.Models;
using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ElCaptain.ApiControllers
{
    [System.Web.Http.RoutePrefix("api/create")]
    public class CreateController : ApiController
    {
        private readonly AppDbContext context;
        private HttpResponseMessage httpResponse;
        public CreateController()
        {
            context = new AppDbContext();
        }

        [HttpPost]
        [Route("orderFinish")]
        public async Task<HttpResponseMessage> OrderFinish(int orderId)
        {
            var items = await context.Orders.FindAsync(orderId);
            items.DeliveryDate = DateTime.UtcNow.AddHours(2);

            items.State = EnumOrderStatus.Done;

            var deliveryMan = await context.DeliveryMen.FindAsync(items.DeliveryManId);
            deliveryMan.Status = EnumDeliveryManStatus.Online;

            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("orderRefuse")]
        public async Task<HttpResponseMessage> OrderRefuse(int orderId)
        {
            //Added to refused Table
            var order = await context.Orders.FindAsync(orderId);
            var refusedOrder = new OrderRefuses()
            {
                OrderId = order.Id,
                Order = order,
                Status = EnumOrderRefusalType.RefusedByDeliveryMan,
                DeliveryManId = order.DeliveryManId.Value
            };
            context.OrderRefuses.Add(refusedOrder);
            await context.SaveChangesAsync();

            //Changed Deliveryman status
            var deliveryMan = await context.DeliveryMen.FindAsync(order.DeliveryManId);
            deliveryMan.Status = EnumDeliveryManStatus.Online;

            //Change order status
            order.State = EnumOrderStatus.TryingToFindDriver;

            //Search for nearest deliveryman ====> Use order Create
            var refeusedIds = await context.OrderRefuses.Where(y => y.OrderId == orderId).Select(x => x.DeliveryManId).ToListAsync();

            var deliveryMen = await context.DeliveryMen.Where(x => x.Status == EnumDeliveryManStatus.Online
           && x.VehicleType == order.VehicleType && !refeusedIds.Contains(x.Id)).ToListAsync();

            if (deliveryMen.Count == 0)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.NotFound);
                return httpResponse;
            }

            var storeCoordinates = new GeoCoordinate(order.FromLatitude, order.FromLongitude);
            GeoCoordinate nearestDeliveryManCoordinates = deliveryMen
                        .Select(x => new GeoCoordinate(x.Latitude, x.Longitude))
                         .OrderBy(x => x.GetDistanceTo(storeCoordinates))
                         .FirstOrDefault();

            var nearestMan = deliveryMen
                 .Where(x => x.Latitude == nearestDeliveryManCoordinates.Latitude && !refeusedIds.Contains(x.Id)
                 && x.Longitude == nearestDeliveryManCoordinates.Longitude).ToList();

            order.DeliveryManId = nearestMan.FirstOrDefault().Id;



            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("orderReached")]
        public async Task<HttpResponseMessage> OrderReached(int orderId)
        {
            var items = await context.Orders.FindAsync(orderId);
            items.State = EnumOrderStatus.Booked;

            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }



        [HttpPost]
        [Route("orderDelete")]
        public async Task<HttpResponseMessage> OrderDelete(int orderId)
        {
            var items = await context.Orders.FindAsync(orderId);
            context.Orders.Remove(items);
            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }


        [HttpPost]
        [Route("orderAccept")]
        public async Task<HttpResponseMessage> OrderAccept(int orderId)
        {
            var items = await context.Orders.FindAsync(orderId);
            items.State = EnumOrderStatus.DriverOnItsWay;

            var deliveryMan = await context.DeliveryMen.FindAsync(items.DeliveryManId);
            //deliveryMan.Status = EnumDeliveryManStatus.Busy;

            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("orderStartTrip")]
        public async Task<HttpResponseMessage> orderStartTrip(int orderId)
        {
            var items = await context.Orders.FindAsync(orderId);
            items.State = EnumOrderStatus.Delivering;
            items.StartDate = DateTime.UtcNow.AddHours(2);

            var deliveryMan = await context.DeliveryMen.FindAsync(items.DeliveryManId);
            deliveryMan.Status = EnumDeliveryManStatus.Busy;

            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("orderDeliveryRate")]
        public async Task<HttpResponseMessage> OrderDeliveryRate(int orderId, int deliveryManRate, string note)
        {
            var items = await context.Orders.FindAsync(orderId);
            items.DeliveryManRating = deliveryManRate;
            items.DeliveryNotes = note;
            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("orderClientRate")]
        public async Task<HttpResponseMessage> OrderClientRate(int orderId, int clientRate, string note)
        {
            var items = await context.Orders.FindAsync(orderId);
            items.ClientRating = clientRate;
            items.ClientNotes = note;
            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("deliveryManUpdateStatus")]
        public async Task<HttpResponseMessage> DeliveryManUpdateStatus(int id, int status)
        {
            var items = await context.DeliveryMen.FindAsync(id);
            items.Status = (EnumDeliveryManStatus)status;

            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }


        [HttpPost]
        [Route("orderUpdateXY")]
        public async Task<HttpResponseMessage> orderUpdateXY(int orderId, double toLat, double toLng)
        {
            var order = await context.Orders.FindAsync(orderId);
            order.ToLatitude = toLat;
            order.ToLongitude = toLng;

            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("deliveryManUpdateLocation")]
        public async Task<HttpResponseMessage> DeliveryManUpdateLocation(int id, double latitude, double longitude)
        {
            var items = await context.DeliveryMen.FindAsync(id);
            items.Latitude = latitude;
            items.Longitude = longitude;

            //var deliverymen = await context.DeliveryMen.ToListAsync();
            // Check deliveyman status
            //foreach (var man in deliverymen)
            //{
            //    TimeSpan interval = man.LastUpdate - DateTime.Now;
            //    if (interval.Minutes > 5)
            //    {
            //        man.Status = EnumDeliveryManStatus.Offline;
            //    }
            //}
            DateTime todaytime = DateTime.Now.ToUniversalTime().AddHours(2);

            var orders = context.Orders.Where(x => x.DeliveryManId == null && x.StartDate <= todaytime).ToList();

            foreach (var order in orders)
            {
                var deliveryMen = await context.DeliveryMen.Where(x => x.Status == EnumDeliveryManStatus.Online
                    && x.VehicleType == order.VehicleType).ToListAsync();

                if (deliveryMen.Count == 0)
                {
                    //httpResponse = Request.CreateResponse(HttpStatusCode.NotFound);
                    //return httpResponse;
                }
                List<DeliveryMan> deliveryMen2 = new List<DeliveryMan>();

                foreach (var man in deliveryMen)
                {
                    var numberOfOrdersToday = await context.Orders.Where(x => x.DeliveryManId == man.Id).CountAsync();
                    var maxNumberOfOrders = man.NumberOfTrips;

                    if (numberOfOrdersToday < maxNumberOfOrders)
                    {
                        deliveryMen2.Add(man);
                    }
                }

                if (deliveryMen2.Count == 0)
                {
                    //httpResponse = Request.CreateResponse(HttpStatusCode.NotFound);
                    //return httpResponse;
                }

                var storeCoordinates = new GeoCoordinate(order.FromLatitude, order.FromLongitude);
                GeoCoordinate nearestDeliveryManCoordinates = deliveryMen2
                            .Select(x => new GeoCoordinate(x.Latitude, x.Longitude))
                             .OrderBy(x => x.GetDistanceTo(storeCoordinates))
                             .FirstOrDefault();

                var nearestMan = deliveryMen2
                     .Where(x => x.Latitude == nearestDeliveryManCoordinates.Latitude
                     && x.Longitude == nearestDeliveryManCoordinates.Longitude).FirstOrDefault();

                order.DeliveryManId = nearestMan.Id;
                nearestMan.Status = EnumDeliveryManStatus.Busy;
            }
            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);

            return httpResponse;
        }



        [HttpPost]
        [Route("clientManUpdateLocation")]
        public async Task<HttpResponseMessage> ClientManUpdateLocation(int id, double latitude, double longitude)
        {
            var items = await context.Clients.FindAsync(id);
            items.Latitude = latitude;
            items.Longitude = longitude;
            await context.SaveChangesAsync();
            httpResponse = Request.CreateResponse(HttpStatusCode.OK);

            return httpResponse;
        }



        [HttpPost]
        [Route("orderCreate")]
        public async Task<HttpResponseMessage> OrderCreate([FromBody] Order order)
        {
            if (order.Date == order.StartDate)
            {
                order.Price = Convert.ToDecimal(CalculateOrderPrice(order));

                var deliveryMen = await context.DeliveryMen.Where(x => x.Status == EnumDeliveryManStatus.Online
                && x.VehicleType == order.VehicleType).ToListAsync();

                if (deliveryMen.Count == 0)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.NotFound);
                    return httpResponse;
                }
                List<DeliveryMan> deliveryMen2 = new List<DeliveryMan>();

                foreach (var man in deliveryMen)
                {
                    var numberOfOrdersToday = await context.Orders.Where(x => x.DeliveryManId == man.Id).CountAsync();
                    var maxNumberOfOrders = man.NumberOfTrips;

                    if (numberOfOrdersToday < maxNumberOfOrders)
                    {
                        deliveryMen2.Add(man);
                    }
                }

                if (deliveryMen2.Count == 0)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.NotFound);
                    return httpResponse;
                }

                var storeCoordinates = new GeoCoordinate(order.FromLatitude, order.FromLongitude);
                GeoCoordinate nearestDeliveryManCoordinates = deliveryMen2
                            .Select(x => new GeoCoordinate(x.Latitude, x.Longitude))
                             .OrderBy(x => x.GetDistanceTo(storeCoordinates))
                             .FirstOrDefault();

                var nearestMan = deliveryMen2
                     .Where(x => x.Latitude == nearestDeliveryManCoordinates.Latitude
                     && x.Longitude == nearestDeliveryManCoordinates.Longitude).FirstOrDefault();

                order.DeliveryManId = nearestMan.Id;
                nearestMan.Status = EnumDeliveryManStatus.Busy;

                order.Price = Convert.ToDecimal(CalculateOrderPrice(order));

                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }
            else
            {
                order.Price = Convert.ToDecimal(CalculateOrderPrice(order));
                order.DeliveryManId = null;
                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }


            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        private double CalculateOrderPrice(Order order)
        {
            var point1 = new GeoCoordinate(order.FromLatitude, order.FromLongitude);
            var point2 = new GeoCoordinate(order.ToLatitude, order.ToLongitude);
            var distance = point1.GetDistanceTo(point2) / 1000;

            var pricing = context.Pricing.FirstOrDefault();

            double price = 0;
            switch (order.VehicleType)
            {
                case EnumVehicleType.TokTok:

                    //price = calculatePrice(EnumVehicleType.TokTok, openCounterPrice: 5, openCounterLimitInKm: 2,
                    //      pricePerPiece: 1, distanceInKm: distance, everyInMeters: 100);

                    price = calculatePrice(EnumVehicleType.TokTok, openCounterPrice: Convert.ToDouble(pricing.Pricing2), openCounterLimitInKm: Convert.ToDouble(pricing.Pricing1),
                           pricePerPiece: Convert.ToDouble(pricing.Pricing4), distanceInKm: distance, everyInMeters: Convert.ToDouble(pricing.Pricing3));
                    break;
                case EnumVehicleType.Tricycle:

                    //price = calculatePrice(EnumVehicleType.TokTok, openCounterPrice: 5, openCounterLimitInKm: 1,
                    //     pricePerPiece: 1, distanceInKm: distance, everyInMeters: 100);
                    price = calculatePrice(EnumVehicleType.TokTok, openCounterPrice: Convert.ToDouble(pricing.Pricing2), openCounterLimitInKm: Convert.ToDouble(pricing.Pricing1),
                          pricePerPiece: Convert.ToDouble(pricing.Pricing4), distanceInKm: distance, everyInMeters: Convert.ToDouble(pricing.Pricing3));

                    break;
                default:
                    break;
            }

            return price;

        }


        /// <summary>
        /// حساب المسافة
        /// </summary>
        /// <param name="vehicleType">نوع المركبة</param>
        /// <param name="openCounterPrice">سعر فتح العداد</param>
        /// <param name="openCounterLimitInKm">ليميت فتح العداد مثلا لحد 1 كيلو</param>
        /// <param name="pricePerPiece">سعر السكشن الواحد</param>
        /// <param name="distanceInKm">المسافة الكلية بالكيلومتر</param>
        /// <param name="everyInMeters"> طول السكشن الواحد بعد ليميت فتح العداد بالمتر</param>
        /// <returns></returns>
        private double calculatePrice(EnumVehicleType vehicleType, double openCounterPrice,
            double openCounterLimitInKm, double pricePerPiece, double distanceInKm, double everyInMeters)
        {
            if (vehicleType == EnumVehicleType.TokTok)
            {
                double distanceAfterOpenLimit = distanceInKm - openCounterLimitInKm;
                if (distanceAfterOpenLimit < 0)
                {
                    return openCounterPrice;
                }
                else
                {
                    double distanceInMeters = distanceAfterOpenLimit * 1000;
                    double price = ((distanceInMeters / everyInMeters) * pricePerPiece) + openCounterPrice;
                    return Math.Ceiling(price);

                }
            }
            else
            {
                return 0;
            }

        }

        [HttpPost]
        [Route("orderCreateFromDelivery")]
        public async Task<HttpResponseMessage> orderCreateFromDelivery([FromBody] Order order)
        {
            var deliveryMen = await context.DeliveryMen.Where(x => x.Id == order.DeliveryManId).ToListAsync();

            deliveryMen[0].Status = EnumDeliveryManStatus.Busy;

            order.Price = Convert.ToDecimal(CalculateOrderPrice(order));

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("orderSpecialCreate")]
        public async Task<HttpResponseMessage> OrderSpecialCreate([FromBody] Order order)
        {
            var deliveryMen = await context.DeliveryMen.ToListAsync();

            var clientCoordinates = new GeoCoordinate(order.FromLatitude, order.FromLatitude);
            GeoCoordinate nearestDeliveryManCoordinates = deliveryMen.Select(x => new GeoCoordinate(x.Latitude, x.Longitude))
                         .OrderBy(x => x.GetDistanceTo(clientCoordinates))
                         .FirstOrDefault();

            var nearestMan = deliveryMen
                 .Where(x => x.Latitude == nearestDeliveryManCoordinates.Latitude
                 && x.Longitude == nearestDeliveryManCoordinates.Longitude).FirstOrDefault();

            order.DeliveryManId = nearestMan.Id;
            //nearestMan.Status = EnumDeliveryManStatus.Busy;

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("clientRegister")]
        public async Task<HttpResponseMessage> ClientRegister([FromBody] Client client)
        {
            client.RegisterDate = DateTime.Now;
            context.Clients.Add(client);
            await context.SaveChangesAsync();

            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }

        [HttpPost]
        [Route("clientUpdate")]
        public async Task<HttpResponseMessage> ClientUpdate([FromBody] Client client)
        {
            var oldClient = context.Clients.Find(client.Id);

            oldClient.Name = client.Name;
            oldClient.City = client.City;
            oldClient.Phone1 = client.Phone1;
            oldClient.Phone2 = client.Phone2;
            oldClient.Address1 = client.Address1;
            oldClient.Address2 = client.Address2;
            oldClient.NearestMonument = client.NearestMonument;
            oldClient.Latitude = client.Latitude;
            oldClient.Longitude = client.Longitude;
            oldClient.NationalId = client.NationalId;
            oldClient.Username = client.Username;
            oldClient.Password = client.Password;

            await context.SaveChangesAsync();

            httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            return httpResponse;
        }
    }
}
