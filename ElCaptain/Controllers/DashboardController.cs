using ElCaptain.Models;
using ElCaptain.Models.Enums;
using ElCaptain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCaptain.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext context;
        private DropDownLists dropDownLists;
        public DashboardController()
        {
            context = new AppDbContext();
            dropDownLists = new DropDownLists();
        }
        [HttpGet]
        public ActionResult Index()
        {
            var dto = new DashboardDto()
            {
                FromDate = DateTime.Now,
                ToDate = DateTime.Now,
                OwnersDropDownList = dropDownLists.VehicleOwnerDropDownListForDashboard(),
                DeliveryMenDropDownList = new List<SelectListItem>()
            };
            dto.NumberOfOrders = context.Orders.Count();
            dto.TotalIncome = context.Orders.ToList().Select(x => x.Price).Sum();
            return View(dto);
        }

        [HttpPost]
        public ActionResult Index(DashboardDto dto)
        {
            var from = new DateTime(dto.FromDate.Year, dto.FromDate.Month, dto.FromDate.Day, 0, 0, 0);
            var to = new DateTime(dto.ToDate.Year, dto.ToDate.Month, dto.ToDate.Day, 23, 59, 59);
            var vehicleType = (Models.Enums.EnumVehicleType)dto.Category;

            //Vehicle Owner = 0
            if (dto.OwnerId == 0)
            {
                dto.NumberOfOrders = context.Orders.Count();
                dto.TotalIncome = context.Orders.Select(x => x.Price).DefaultIfEmpty(0).Sum();
            }
            else
            {
                if (dto.DeliveryManId == 0)
                {
                    dto.NumberOfOrders = context.Orders.Where(x => x.DeliveryMan.VehicleOwnerId == dto.OwnerId
                    && x.VehicleType == vehicleType
                    && x.Date >= from && x.Date <= to).Count();


                    dto.TotalIncome = context.Orders.Where(x => x.DeliveryMan.VehicleOwnerId == dto.OwnerId
                    && x.VehicleType == vehicleType
                    && x.Date >= from && x.Date <= to).Select(x => x.Price).DefaultIfEmpty(0).Sum();
                }
                else
                {
                    dto.NumberOfOrders = context.Orders.Where(x => x.DeliveryManId == dto.DeliveryManId
                  && x.DeliveryMan.VehicleOwnerId == dto.OwnerId && x.VehicleType == vehicleType
                && x.Date >= from && x.Date <= to).Count();


                    dto.TotalIncome = context.Orders.Where(x => x.DeliveryManId == dto.DeliveryManId
                    && x.DeliveryMan.VehicleOwnerId == dto.OwnerId && x.VehicleType == vehicleType
                    && x.Date >= from && x.Date <= to).Select(x => x.Price).DefaultIfEmpty(0).Sum();
                }

            }



            dto.DeliveryMenDropDownList = new List<SelectListItem>();

            dto.OwnersDropDownList = dropDownLists.VehicleOwnerDropDownListForDashboard();



            return View(dto);
        }


        [HttpGet]
        public JsonResult GetStores(int categoryId)
        {
            var result = new JsonResult();
            var category = (Models.Enums.EnumStoreCategory)categoryId;
            var stores = context.Stores.Where(x => x.Category == category).ToList();
            result = Json(stores, JsonRequestBehavior.AllowGet);
            return result;
        }


        [HttpGet]
        public JsonResult GetDeliveryMen(int ownerId, int categoryId)
        {
            var result = new JsonResult();
            var category = (EnumVehicleType)categoryId;
            var deliveryMen = context.DeliveryMen.Where(x => x.VehicleOwnerId == ownerId && x.VehicleType == category).ToList();

            deliveryMen.Add(new DeliveryMan()
            {
                Id = 0,
                Username = "الكل"
            });
            var temp = deliveryMen[0];
            deliveryMen[0] = deliveryMen[deliveryMen.Count - 1];
            deliveryMen[deliveryMen.Count - 1] = temp;

            result = Json(deliveryMen, JsonRequestBehavior.AllowGet);
            return result;
        }



    }
}