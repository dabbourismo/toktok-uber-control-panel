using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCaptain.Services
{
    public class DropDownLists
    {
        private readonly AppDbContext context;
        public DropDownLists()
        {
            context = new AppDbContext();
        }
        public List<SelectListItem> StoresDropDownList()
        {
            var list = new List<SelectListItem>();
            var items = context.Stores.Select(c => new { c.Id, c.Name }).ToList();

            foreach (var item in items)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });

            }
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem {Text = "لا يوجد سائقين مسجلين",Value = null}
                };
            }
        }


        public List<SelectListItem> StoresDropDownListForDashboard(int categoryId)
        {
            var list = new List<SelectListItem>();
            var items = context.Stores.Where(x => x.Category == (Models.Enums.EnumStoreCategory)categoryId).Select(c => new { c.Id, c.Name }).ToList();
            foreach (var item in items)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });

            }
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem {Text = "لا يوجد سائقين مسجلة",Value = null}
                };
            }
        }


        public List<SelectListItem> DeliveryMenDropDownList()
        {
            var list = new List<SelectListItem>();
            var items = context.DeliveryMen.Select(c => new { c.Id, c.Name }).ToList();

            foreach (var item in items)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });

            }
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem {Text = "لا يوجد سائقين مسجلين",Value = null}
                };
            }
        }

        public List<SelectListItem> VehicleOwnerDropDownListForDashboard()
        {
            var list = new List<SelectListItem>();
            var items = context.VehicleOwners.Select(c => new { c.Id, c.Name }).ToList();

            list.Add(new SelectListItem
            {
                Text = "الكل",
                Value = "0"
            });


            foreach (var item in items)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });

            }
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem {Text = "لا يوجد ملاك مسجلين ",Value = null}
                };
            }
        }


        public List<SelectListItem> VehicleOwnerDropDownList()
        {
            var list = new List<SelectListItem>();
            var items = context.VehicleOwners.Select(c => new { c.Id, c.Name }).ToList();



            foreach (var item in items)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });

            }
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem {Text = "لا يوجد ملاك مسجلين ",Value = null}
                };
            }
        }
    }
}