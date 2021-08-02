using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElCaptain.Models;
using ElCaptain.Services;
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
    public class NotificationController : Controller
    {
        private readonly AppDbContext context;
        private DropDownLists dropDownLists;

        public NotificationController()
        {
            context = new AppDbContext();
            dropDownLists = new DropDownLists();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ViewAllNotifications()
        {
            //Server side parameters
            List<NotificationDto> NotificationsList;
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
                NotificationsList = await context.Notifications
                    .Where(x => x.Name.Contains(searchValue))
                    .ProjectTo<NotificationDto>().ToListAsync();
                totalRows = NotificationsList.Count;
                totalFilteredRows = NotificationsList.Count;
            }
            else
            {
                NotificationsList = await context.Notifications.ProjectTo<NotificationDto>().ToListAsync();
                totalRows = NotificationsList.Count;
                totalFilteredRows = NotificationsList.Count;
            }

            //Sorting
            NotificationsList = NotificationsList.OrderBy(sortColumnName + " " + sortDirection).ToList();
            //Paging
            NotificationsList = NotificationsList.Skip(start).Take(length).ToList();

            return Json(new { data = NotificationsList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> AddEditNotification(int id = 0)
        {
            //Add operation
            switch (id)
            {
                case 0:
                    return View(new NotificationDto());
                default:
                    var Notification = await context.Notifications.FindAsync(id);
                    var dto = Mapper.Map<Notification, NotificationDto>(Notification);
                    return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditNotification(NotificationDto dto)
        {

            var Notification = Mapper.Map<NotificationDto, Notification>(dto);

            //add operation
            switch (dto.Id)
            {
                case 0:
                    context.Notifications.Add(Notification);
                    await context.SaveChangesAsync();
                    return Json(new { success = true, message = "تم اضافة الاشعار بنجاح" }, JsonRequestBehavior.AllowGet);
                default:
                    context.Entry(Notification).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Json(new { success = true, message = "تم تعديل الاشعار بنجاح" }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult DeleteNotification(int Id)
        {
            var entity = context.Notifications.Find(Id);
            context.Notifications.Remove(entity);
            context.SaveChanges();
            return Json(new { success = true, message = "تم الحذف بنجاح" }, JsonRequestBehavior.AllowGet);
        }
    }
}