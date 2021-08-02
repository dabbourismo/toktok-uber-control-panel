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

namespace ElCaptain.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDbContext context;

        public PaymentController()
        {
            context = new AppDbContext();
        }
        public ActionResult Index()
        {
            var dto = new SearchDto()
            {
                FromDate = DateTime.Now,
                ToDate = DateTime.Now
            };
            return View(dto);
        }


        [HttpPost]
        public async Task<ActionResult> ViewAllPayments()
        {
            //Server side parameters
            List<PaymentDto> PaymentList;
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);

            string searchValue = Request["search[value]"];

            var startDate = DateTime.Parse(Request["startDate"]);
            var endDate = DateTime.Parse(Request["endDate"]);

            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            int totalRows = 0;
            int totalFilteredRows = 0;


            //Searching
            if (!string.IsNullOrEmpty(searchValue))
            {
                PaymentList = await context.Payments
                    .Where(x =>
                    (x.Order.Client.Name.Contains(searchValue))
                    && ((x.Date >= startDate) && (x.Date <= endDate)))
                    .Include(x=>x.Order)
                    .Include(x=>x.Order.Client)
                    .ProjectTo<PaymentDto>().ToListAsync();

                totalRows = PaymentList.Count;
                totalFilteredRows = PaymentList.Count;
            }
            else
            {
                //Paging
                PaymentList = await context.Payments
                .Where(x => x.Date >= startDate && x.Date <= endDate)
                .Include(x => x.Order)
                 .OrderBy(x => x.Id).Skip(start).Take(length).ProjectTo<PaymentDto>().ToListAsync();

                totalRows = context.Payments.Count();
                totalFilteredRows = context.Payments.Count();
            }

            //Sorting
            PaymentList = PaymentList.OrderBy(sortColumnName + " " + sortDirection).ToList();


            return Json(new { data = PaymentList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
        }
    }
}