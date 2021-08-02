using AutoMapper;
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
    public class ClientsController : Controller
    {
        private readonly AppDbContext context;

        public ClientsController()
        {
            context = new AppDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ViewAllClients()
        {
            //Server side parameters
            List<ClientDto> clientsList;
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
                clientsList = await context.Clients
                    .Where(x => x.Name.Contains(searchValue)
                    || (x.Username.Contains(searchValue))
                    || (x.Password.Contains(searchValue))
                    || (x.Phone1.Contains(searchValue))
                    || (x.Phone2.Contains(searchValue))
                    || (x.Address1.Contains(searchValue))
                    || (x.Address2.Contains(searchValue))
                    || (x.NearestMonument.Contains(searchValue))
                    || (x.NationalId.Equals(searchValue)))                    
                    .ProjectTo<ClientDto>().ToListAsync();
                totalRows = clientsList.Count;
                totalFilteredRows = clientsList.Count;
            }
            else
            {
                //Paging
                clientsList = await context.Clients.OrderBy(x=>x.Id).Skip(start).Take(length).ProjectTo<ClientDto>().ToListAsync();                
                totalRows = context.Clients.Count();
                totalFilteredRows = context.Clients.Count();
            }

            //Sorting
            clientsList = clientsList.OrderBy(sortColumnName + " " + sortDirection).ToList();

            //Paging
            //clientsList = clientsList.Skip(start).Take(length).ToList();


            foreach (var client in clientsList)
            {
                client.TotalOrders = context.Orders.Where(x => x.ClientId == client.Id).Count();
                client.LastOrderDate = context.Orders.Where(x => x.ClientId == client.Id).OrderByDescending(x => x.Date).FirstOrDefault()?.Date;
            }

            return Json(new { data = clientsList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<ActionResult> AddEditClient(int id = 0)
        {
            switch (id)
            {
                case 0:
                    return View(new ClientDto());
                default:
                    var Client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
                    var dto = Mapper.Map<Client, ClientDto>(Client);
                    return View(dto);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditClient(ClientDto ClientDto)
        {
            var Client = Mapper.Map<ClientDto, Client>(ClientDto);
            //add operation
            switch (ClientDto.Id)
            {
                case 0:
                    context.Clients.Add(Client);
                    await context.SaveChangesAsync();
                    return Json(new { success = true, message = "تم اضافة العميل بنجاح" }, JsonRequestBehavior.AllowGet);
                default:
                    context.Entry(Client).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Json(new { success = true, message = "تم تعديل العميل بنجاح" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<JsonResult> DeleteClient(int Id)
        {
            var entity = await context.Clients.FindAsync(Id);
            context.Clients.Remove(entity);
            await context.SaveChangesAsync();
            return Json(new { success = true, message = "تم حذف العميل بنجاح" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ToggleClientActive(int Id)
        {
            var Client = context.Clients.Find(Id);
            Client.isActive = !Client.isActive;
            context.SaveChanges();
            return Json(new { success = true, message = "تم تغيير التفعيل" }, JsonRequestBehavior.AllowGet);
        }
    }
}