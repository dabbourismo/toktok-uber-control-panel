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
    [System.Web.Http.RoutePrefix("api/select")]
    public class SelectController : ApiController
    {
        private readonly AppDbContext context;
        public SelectController()
        {
            context = new AppDbContext();
        }

        [HttpGet]
        [Route("pricingGet")]
        public async Task<Pricing> PricingGet()
        {
            var items = await context.Pricing
                .Where(x => x.Id == 1).FirstOrDefaultAsync();

            return items;
        }

        [HttpGet]
        [Route("deliveryManGetStatus")]
        public async Task<int> DeliveryManGetStatus(int deliveryManId)
        {
            var items = await context.DeliveryMen
                .Where(x => x.Id == deliveryManId).FirstOrDefaultAsync();

            return (int)items.Status;
        }


        [HttpGet]
        [Route("OrderPriceGet")]
        public async Task<double> OrderPriceGet(EnumVehicleType vehicletype, double fromx, double fromy, double tox, double toy)
        {
            var point1 = new GeoCoordinate(fromx, fromy);
            var point2 = new GeoCoordinate(tox, toy);
            var distance = point1.GetDistanceTo(point2) / 1000;
            var pricing = context.Pricing.FirstOrDefault();

            double price = 0;
            switch (vehicletype)
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


        [HttpGet]
        [Route("orderDetailGet")]
        public async Task<Order> OrderDetailGet(int orderId)
        {
            var items = await context.Orders
                .Where(x => x.Id == orderId)
                .Include(x => x.DeliveryMan)
                .Include(x => x.Client)
                .Include(x => x.Store)
                .FirstOrDefaultAsync();
            return items;
        }

        [HttpGet]
        [Route("deliveryManRatingGet")]
        public async Task<int> DeliveryManRatingGet(int deliveryManId)
        {
            var items = await context.DeliveryMen
                .Where(x => x.Id == deliveryManId).FirstOrDefaultAsync();

            var numberOfOrders = context.Orders.Where(x => x.DeliveryManId == deliveryManId).ToList().Count;
            var sumOfRatings = context.Orders.Where(x => x.DeliveryManId == deliveryManId)
                .Select(x => x.DeliveryManRating).DefaultIfEmpty(0).Sum();
            var average = sumOfRatings / numberOfOrders;

            return average;
        }


        [HttpGet]
        [Route("clientRatingGet")]
        public async Task<int> ClientRatingGet(int clientId)
        {
            var items = await context.Clients
                .Where(x => x.Id == clientId).FirstOrDefaultAsync();

            var numberOfOrders = context.Orders.Where(x => x.ClientId == clientId).ToList().Count;
            var sumOfRatings = context.Orders.Where(x => x.ClientId == clientId)
                .Select(x => x.ClientRating).DefaultIfEmpty(0).Sum();
            var average = sumOfRatings / numberOfOrders;

            return average;
        }

        [HttpGet]
        [Route("clientLogin")]
        public async Task<Client> ClientLogin(string username, string password)
        {
            if (username.StartsWith("01"))
            {
                var items = await context.Clients
                           .Where(x => x.Phone1 == username && x.Password == password).FirstOrDefaultAsync();
                return items;
            }
            else
            {
                var items = await context.Clients
                              .Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
                return items;
            }

        }




        [HttpGet]
        [Route("deliveryManLogin")]
        public async Task<DeliveryMan> DeliveryManLogin(string username, string password)
        {
            if (username.StartsWith("01"))
            {

                var items = await context.DeliveryMen
                .Where(x => x.Phone1 == username && x.Password == password).FirstOrDefaultAsync();
                return items;
            }

            else
            {

                var items = await context.DeliveryMen
                .Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
                return items;
            }
        }

        [HttpGet]
        [Route("deliveryMenGetLocation")]
        public async Task<List<DeliveryMan>> DeliveryMenGetLocation(double latitude, double longitude)
        {
            var items = await context.DeliveryMen
                .Where(x => x.Latitude == latitude && x.Longitude == longitude).ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("storesGetByCategory")]
        public async Task<List<Store>> StoreGetByCategory(EnumStoreCategory category)
        {
            var items = await context.Stores
                .Where(x => x.Category == category)
                .OrderBy(x => x.Name)
                .ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("storesSearchByName")]
        public async Task<List<Store>> StoresSearchByName(string name)
        {
            var items = await context.Stores
                .Where(x => x.Name.Contains(name.ToLower()))
                .OrderBy(x => x.Name)
                .ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("ordersGetByDateForDeliveryMan")]
        public async Task<List<Order>> OrdersGetByDateForDeliveryMan(int deliveryManId, DateTime from, DateTime to)
        {
            var items = await context.Orders
                .Where(x => x.DeliveryManId == deliveryManId && (x.Date >= from && x.Date <= to))
                .Include(x => x.Store)
                .Include(x => x.Client)
                .Include(x => x.DeliveryMan)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            var orders = new List<Order>();
            foreach (var order in items)
            {
                var refusedOrder = context.OrderRefuses.Where(x => x.OrderId == order.Id).FirstOrDefault();
                if (refusedOrder != null)
                {
                    //items.RemoveAll(x => x.Id == refusedOrder.OrderId);
                }
                else
                {
                    orders.Add(order);
                }
            }
            return orders;
        }


        [HttpGet]
        [Route("ordersGetNew")]
        public async Task<List<Order>> OrdersGetNew(int deliveryManId)
        {
            var items = await context.Orders
                .Where(x => x.DeliveryManId == deliveryManId && x.State == EnumOrderStatus.TryingToFindDriver)
                .Include(x => x.Store)
                .Include(x => x.DeliveryMan)
                .Include(x => x.Client)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            foreach (var order in items)
            {
                var refusedOrder = context.OrderRefuses.Where(x => x.OrderId == order.Id).FirstOrDefault();
                if (refusedOrder != null)
                {
                    items.RemoveAll(x => x.Id == refusedOrder.OrderId);
                }
            }
            return items;
        }


        [HttpGet]
        [Route("ordersGetAccepted")]
        public async Task<List<Order>> OrdersGetAccepted(int clientId)
        {
            var items = await context.Orders
                .Where(x => x.ClientId == clientId && x.State == EnumOrderStatus.DriverOnItsWay)
                .Include(x => x.DeliveryMan)
                .Include(x => x.Client)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            foreach (var order in items)
            {
                var refusedOrder = context.OrderRefuses.Where(x => x.OrderId == order.Id).FirstOrDefault();
                if (refusedOrder != null)
                {
                    items.RemoveAll(x => x.Id == refusedOrder.OrderId);
                }
            }
            return items;
        }


        [HttpGet]
        [Route("ordersGetArrived")]
        public async Task<List<Order>> ordersGetArrived(int clientId)
        {
            var items = await context.Orders
                .Where(x => x.ClientId == clientId && x.State == EnumOrderStatus.Delivering)
                .Include(x => x.Store)
                .Include(x => x.DeliveryMan)
                .Include(x => x.Client)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            foreach (var order in items)
            {
                var refusedOrder = context.OrderRefuses.Where(x => x.OrderId == order.Id).FirstOrDefault();
                if (refusedOrder != null)
                {
                    items.RemoveAll(x => x.Id == refusedOrder.OrderId);
                }
            }
            return items;
        }



        [HttpGet]
        [Route("ordersGet")]
        public async Task<List<Order>> OrdersGet(int clientId)
        {
            var items = await context.Orders
                .Where(x => x.ClientId == clientId)
                .Include(x => x.Store)
                .Include(x => x.DeliveryMan)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("menusGet")]
        public async Task<List<Menu>> MenusGet(int storeId)
        {
            var items = await context.Menus
                .Where(x => x.StoreId == storeId && x.isOffered == false)
                .Include(x => x.Store)
                .ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("menusGetIsOffered")]
        public async Task<List<Menu>> MenusGetIsOffered()
        {
            var items = await context.Menus
                .Where(x => x.isOffered == true)
                .Include(x => x.Store)
                .ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("notifGet")]
        public async Task<List<Notification>> NotificationsGet(int notifType)
        {
            if (notifType == 0)
            {
                IQueryable<Notification> query = context.Notifications
                    .Where(x => ((int)x.NotifType == (int)EnumNotifType.Client) && (x.ExpireDate >= DateTime.Now));
                var result = await query.ToListAsync();
                return result;
            }
            else
            {
                IQueryable<Notification> query = context.Notifications
                       .Where(x => ((int)x.NotifType == (int)EnumNotifType.DeliveryMan) && (x.ExpireDate >= DateTime.Now));
                var result = await query.ToListAsync();
                return result;
            }

        }

    }
}
