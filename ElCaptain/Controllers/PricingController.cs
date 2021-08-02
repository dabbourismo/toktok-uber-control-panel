using AutoMapper.QueryableExtensions;
using ElCaptain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using AutoMapper;

namespace ElCaptain.Controllers
{
    public class PricingController : Controller
    {
        private readonly AppDbContext context;
        public PricingController()
        {
            context = new AppDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ViewAllPricings()
        {
            //Server side parameters
            List<PricingDto> PricingsList;
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            int totalRows = 0;
            int totalFilteredRows = 0;


            //Searching
            if (!string.IsNullOrEmpty(searchValue))
            {
                PricingsList = await context.Pricing
                    .Where(x => x.Pricing1.ToString().Contains(searchValue))
                    .ProjectTo<PricingDto>().ToListAsync();
                totalRows = PricingsList.Count;
                totalFilteredRows = PricingsList.Count;
            }
            else
            {
                PricingsList = await context.Pricing.ProjectTo<PricingDto>().ToListAsync();
                totalRows = PricingsList.Count;
                totalFilteredRows = PricingsList.Count;
            }

            //Sorting
            PricingsList = PricingsList.OrderBy(sortColumnName + " " + sortDirection).ToList();
            //Paging
            PricingsList = PricingsList.Skip(start).Take(length).ToList();

            return Json(new { data = PricingsList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<ActionResult> AddEditPricing(int id = 0)
        {
            //Add operation
            switch (id)
            {
                case 0:
                    return View(new PricingDto());
                default:
                    var Pricing = await context.Pricing.FindAsync(id);
                    var dto = Mapper.Map<Pricing, PricingDto>(Pricing);
                    return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditPricing(PricingDto dto)
        {

            var Pricing = Mapper.Map<PricingDto, Pricing>(dto);

            //add operation
            switch (dto.Id)
            {
                case 0:
                    context.Pricing.Add(Pricing);
                    await context.SaveChangesAsync();
                    return Json(new { success = true, message = "تم اضافة الاشعار بنجاح" }, JsonRequestBehavior.AllowGet);
                default:
                    context.Entry(Pricing).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Json(new { success = true, message = "تم تعديل التسعير بنجاح" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}