using AutoMapper.QueryableExtensions;
using ElCaptain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Linq.Dynamic;
using System.Web.Mvc;
using AutoMapper;
using ElCaptain.Services;

namespace ElCaptain.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext context;
        private DropDownLists dropDownLists;
        public OrderController()
        {
            context = new AppDbContext();
            dropDownLists = new DropDownLists();
        }
        public ActionResult Index()
        {
            var dto = new SearchDto()
            {
                FromDate = DateTime.Now,
                ToDate = DateTime.Now
            };

            try
            {
                dto.NumberOfOrders = context.Orders.Count();
                dto.OrdersIncome = context.Orders.ToList().Select(x => x.Price).Sum();
            }
            catch (Exception)
            {
                dto.NumberOfOrders = 0;

                dto.OrdersIncome = 0;
            }


            return View(dto);
        }

        [HttpPost]
        public async Task<ActionResult> ViewAllOrders()
        {
            //Server side parameters
            List<OrderDto> orderList;
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchQuery = Request["searchQuery"];


            //string searchValue = Request["search[value]"];

            var startDate = DateTime.Parse(Request["startDate"]);
            var endDate = DateTime.Parse(Request["endDate"]);

            var from = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
            var to = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);


            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            int totalRows = 0;
            int totalFilteredRows = 0;


            var userId = (int)Session["userId"];

            if (Convert.ToInt32(userId) == 0)
            {
                //Searching
                if (!string.IsNullOrEmpty(searchQuery))
                {

                    orderList = await context.Orders
                        .Where(x =>
                        (x.Client.Name.Contains(searchQuery)
                        || (x.Id.ToString().Contains(searchQuery))
                        || (x.Client.Phone1.Contains(searchQuery))

                        || (x.Store.Name.Contains(searchQuery))
                        || (x.Store.Phone1.Contains(searchQuery))

                        || (x.DeliveryMan.Name.Contains(searchQuery))
                        || (x.DeliveryMan.Phone2.Contains(searchQuery)))
                        && ((x.Date >= from) && (x.Date <= to)))
                        .Include(x => x.Client)
                        .Include(x => x.Store)
                        .Include(x => x.DeliveryMan)
                        .ProjectTo<OrderDto>().ToListAsync();



                    totalRows = orderList.Count;
                    totalFilteredRows = orderList.Count;
                }
                else
                {
                    //Paging
                    orderList = await context.Orders
                        .Where(x => x.Date >= from && x.Date <= to)
                        .Include(x => x.Client)
                        .Include(x => x.Store)
                        .Include(x => x.DeliveryMan)
                        .OrderBy(x => x.Id).Skip(start).Take(length).ProjectTo<OrderDto>().ToListAsync();

                    totalRows = context.Orders.Count();
                    totalFilteredRows = context.Orders.Count();
                }


                foreach (var order in orderList)
                {
                    order.TravelTime = Math.Floor((order.DeliveryDate - order.StartDate).TotalHours);
                }


                //Sorting
                orderList = orderList.OrderBy(sortColumnName + " " + sortDirection).ToList();
                return Json(new { data = orderList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
            }

            if (Convert.ToInt32(userId) > 0)
            {
                //Searching
                if (!string.IsNullOrEmpty(searchQuery))
                {

                    orderList = await context.Orders
                        .Where(x =>
                        (x.Client.Name.Contains(searchQuery)
                        || (x.Id.ToString().Contains(searchQuery))
                        || (x.Client.Phone1.Contains(searchQuery))

                        || (x.Store.Name.Contains(searchQuery))
                        || (x.Store.Phone1.Contains(searchQuery))

                        || (x.DeliveryMan.Name.Contains(searchQuery))
                        || (x.DeliveryMan.Phone2.Contains(searchQuery)) 
                        && x.DeliveryMan.VehicleOwnerId == Convert.ToInt32(userId))
                        && ((x.Date >= from) && (x.Date <= to)))
                        .Include(x => x.Client)
                        .Include(x => x.Store)
                        .Include(x => x.DeliveryMan)
                        .ProjectTo<OrderDto>().ToListAsync();



                    totalRows = orderList.Count;
                    totalFilteredRows = orderList.Count;
                }
                else
                {
                    //Paging
                    orderList = await context.Orders
                        .Where(x => x.Date >= from && x.Date <= to)
                        .Include(x => x.Client)
                        .Include(x => x.Store)
                        .Include(x => x.DeliveryMan)
                        .OrderBy(x => x.Id).Skip(start).Take(length).ProjectTo<OrderDto>().ToListAsync();

                    totalRows = context.Orders.Count();
                    totalFilteredRows = context.Orders.Count();
                }
                foreach (var order in orderList)
                {
                    order.TravelTime = (order.StartDate - order.DeliveryDate).TotalHours;
                }
                //Sorting
                orderList = orderList.OrderBy(sortColumnName + " " + sortDirection).ToList();
                return Json(new { data = orderList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
            }

            return View();




        }


        [HttpGet]
        public async Task<ActionResult> AddEditOrder(int id = 0)
        {
            switch (id)
            {
                case 0:
                    return View(new OrderDto()
                    {
                        DeliveryMenDropDownList = dropDownLists.DeliveryMenDropDownList()
                    });
                default:
                    var Order = await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
                    var dto = Mapper.Map<Order, OrderDto>(Order);
                    dto.DeliveryMenDropDownList = dropDownLists.DeliveryMenDropDownList();
                    SelectListItem selectedStore
                         = dto.DeliveryMenDropDownList.Find(e => e.Value == dto.DeliveryManId.ToString());
                    selectedStore.Selected = true;
                    return View(dto);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditOrder(OrderDto OrderDto)
        {
            var Order = Mapper.Map<OrderDto, Order>(OrderDto);
            //add operation
            switch (OrderDto.Id)
            {
                case 0:
                    context.Orders.Add(Order);
                    await context.SaveChangesAsync();
                    return Json(new { success = true, message = "تم اضافة الرحلة بنجاح" }, JsonRequestBehavior.AllowGet);
                default:
                    var order = context.Orders.Find(OrderDto.Id);
                    //context.Entry(Order).State = EntityState.Modified;
                    order.DeliveryManId = OrderDto.DeliveryManId;
                    await context.SaveChangesAsync();
                    return Json(new { success = true, message = "تم تعديل السائق بنجاح" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<JsonResult> DeleteOrder(int Id)
        {
            var entity = await context.Orders.FindAsync(Id);
            context.Orders.Remove(entity);
            await context.SaveChangesAsync();
            return Json(new { success = true, message = "تم حذف الرحلة بنجاح" }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ToggleOrderActive(int Id)
        {
            var order = context.Orders.Find(Id);
            order.State = Models.Enums.EnumOrderStatus.Done;
            context.SaveChanges();
            return Json(new { success = true, message = "تم انهاء الرحلة" }, JsonRequestBehavior.AllowGet);
        }
    }
}