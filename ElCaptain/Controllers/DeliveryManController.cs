using ElCaptain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Data.Entity;
using ElCaptain.Services;

namespace ElCaptain.Controllers
{
    public class DeliveryManController : Controller
    {

        private readonly AppDbContext context;
        public DropDownLists dropDownLists;

        public DeliveryManController()
        {
            context = new AppDbContext();
            dropDownLists = new DropDownLists();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ViewAllDeliveryMan()
        {
            //Server side parameters
            List<DeliveryManDto> DeliveryMansList;
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            int totalRows = 0;
            int totalFilteredRows = 0;

            var userId = (int)Session["userId"];
     

            if (Convert.ToInt32(userId) == 0)
            {
                //Searching
                if (!string.IsNullOrEmpty(searchValue))
                {
                    DeliveryMansList = await context.DeliveryMen
                        .Where(x => x.Name.Contains(searchValue)
                        || (x.Name.Contains(searchValue))
                        || (x.NationalId.Contains(searchValue))
                        || (x.Phone1.Contains(searchValue))
                        || (x.Phone2.Contains(searchValue))
                        || (x.Address.Contains(searchValue))
                        || (x.LandPhone.Contains(searchValue))
                        || (x.Rating.ToString().Contains(searchValue)))
                        .ProjectTo<DeliveryManDto>().ToListAsync();
                    totalRows = DeliveryMansList.Count;
                    totalFilteredRows = DeliveryMansList.Count;
                }
                else
                {
                    DeliveryMansList = await context.DeliveryMen.ProjectTo<DeliveryManDto>().ToListAsync();
                    totalRows = DeliveryMansList.Count;
                    totalFilteredRows = DeliveryMansList.Count;
                }

                //Sorting
                DeliveryMansList = DeliveryMansList.OrderBy(sortColumnName + " " + sortDirection).ToList();

                //Paging
                DeliveryMansList = DeliveryMansList.Skip(start).Take(length).ToList();

                return Json(new { data = DeliveryMansList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
            }

            if (Convert.ToInt32(userId) > 0)
            {
                //Searching
                if (!string.IsNullOrEmpty(searchValue))
                {
                    DeliveryMansList = await context.DeliveryMen
                        .Where(x => x.Name.Contains(searchValue)
                        || (x.Name.Contains(searchValue))
                        || (x.NationalId.Contains(searchValue))
                        || (x.Phone1.Contains(searchValue))
                        || (x.Phone2.Contains(searchValue))
                        || (x.Address.Contains(searchValue))
                        || (x.LandPhone.Contains(searchValue))
                        || (x.Rating.ToString().Contains(searchValue)) && x.VehicleOwnerId == Convert.ToInt32(userId))
                        .ProjectTo<DeliveryManDto>().ToListAsync();
                    totalRows = DeliveryMansList.Count;
                    totalFilteredRows = DeliveryMansList.Count;
                }
                else
                {
                    DeliveryMansList = await context.DeliveryMen.ProjectTo<DeliveryManDto>().ToListAsync();
                    totalRows = DeliveryMansList.Count;
                    totalFilteredRows = DeliveryMansList.Count;
                }

                //Sorting
                DeliveryMansList = DeliveryMansList.OrderBy(sortColumnName + " " + sortDirection).ToList();

                //Paging
                DeliveryMansList = DeliveryMansList.Skip(start).Take(length).ToList();

                return Json(new { data = DeliveryMansList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }


        [HttpGet]
        public async Task<ActionResult> AddEditDeliveryMan(int id = 0)
        {
            switch (id)
            {
                case 0:
                    return View(new DeliveryManDto()
                    {
                        VehicleOwnerDropDownList = dropDownLists.VehicleOwnerDropDownList()
                    });
                default:
                    var DeliveryMan = await context.DeliveryMen.FirstOrDefaultAsync(x => x.Id == id);
                    var dto = Mapper.Map<DeliveryMan, DeliveryManDto>(DeliveryMan);

                    dto.VehicleOwnerDropDownList = dropDownLists.VehicleOwnerDropDownList();
                    SelectListItem selecteditem
                        = dto.VehicleOwnerDropDownList.Find(e => e.Value == dto.VehicleOwnerId.ToString());
                    selecteditem.Selected = true;

                    return View(dto);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditDeliveryMan(DeliveryManDto DeliveryManDto)
        {
            var DeliveryMan = Mapper.Map<DeliveryManDto, DeliveryMan>(DeliveryManDto);
            //add operation
            switch (DeliveryManDto.Id)
            {
                case 0:
                    context.DeliveryMen.Add(DeliveryMan);
                    await context.SaveChangesAsync();


                    //رخصة القيادة
                    if (DeliveryManDto.ImageUpload1 != null)
                    {
                        FileUploader.UploadFTPFileStringName(DeliveryManDto.ImageUpload1.FileName
                            , DeliveryMan.Id.ToString() + "-" + "1"
                            , DeliveryManDto.ImageUpload1
                            , Server.MapPath("~/DriverImages"));
                    }
                    //صورة البطاقة
                    if (DeliveryManDto.ImageUpload2 != null)
                    {
                        FileUploader.UploadFTPFileStringName(DeliveryManDto.ImageUpload2.FileName
                            , DeliveryMan.Id + "-" + "2"
                            , DeliveryManDto.ImageUpload2
                            , Server.MapPath("~/DriverImages"));
                    }
                    //صورة رخصة المركبة
                    if (DeliveryManDto.ImageUpload3 != null)
                    {
                        FileUploader.UploadFTPFileStringName(DeliveryManDto.ImageUpload3.FileName
                            , DeliveryMan.Id + "-" + "3"
                            , DeliveryManDto.ImageUpload3
                            , Server.MapPath("~/DriverImages"));
                    }
                    //
                    if (DeliveryManDto.ImageUpload4 != null)
                    {
                        FileUploader.UploadFTPFileStringName(DeliveryManDto.ImageUpload4.FileName
                            , DeliveryMan.Id + "-" + "4"
                            , DeliveryManDto.ImageUpload4
                            , Server.MapPath("~/DriverImages"));
                    }


                    return Json(new { success = true, message = "تم اضافة السائق بنجاح" }, JsonRequestBehavior.AllowGet);
                default:
                    context.Entry(DeliveryMan).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    //رخصة القيادة
                    if (DeliveryManDto.ImageUpload1 != null)
                    {
                        FileUploader.DeleteFTPFileStringName(DeliveryMan.Id.ToString() + "-" + "1", Server.MapPath("~/DriverImages"));
                        FileUploader.UploadFTPFileStringName(DeliveryManDto.ImageUpload1.FileName
                            , DeliveryMan.Id.ToString() + "-" + "1"
                            , DeliveryManDto.ImageUpload1
                            , Server.MapPath("~/DriverImages"));
                    }

                    //صورة البطاقة
                    if (DeliveryManDto.ImageUpload2 != null)
                    {
                        FileUploader.DeleteFTPFileStringName(DeliveryMan.Id.ToString() + "-" + "2", Server.MapPath("~/DriverImages"));
                        FileUploader.UploadFTPFileStringName(DeliveryManDto.ImageUpload2.FileName
                            , DeliveryMan.Id.ToString() + "-" + "2"
                            , DeliveryManDto.ImageUpload2
                            , Server.MapPath("~/DriverImages"));
                    }

                    //صورة رخصة المركبة
                    if (DeliveryManDto.ImageUpload3 != null)
                    {
                        FileUploader.DeleteFTPFileStringName(DeliveryMan.Id.ToString() + "-" + "3", Server.MapPath("~/DriverImages"));
                        FileUploader.UploadFTPFileStringName(DeliveryManDto.ImageUpload3.FileName
                            , DeliveryMan.Id.ToString() + "-" + "3"
                            , DeliveryManDto.ImageUpload3
                            , Server.MapPath("~/DriverImages"));
                    }

                    //صورة رخصة المركبة
                    if (DeliveryManDto.ImageUpload4 != null)
                    {
                        FileUploader.DeleteFTPFileStringName(DeliveryMan.Id.ToString() + "-" + "4", Server.MapPath("~/DriverImages"));
                        FileUploader.UploadFTPFileStringName(DeliveryManDto.ImageUpload4.FileName
                            , DeliveryMan.Id.ToString() + "-" + "3"
                            , DeliveryManDto.ImageUpload4
                            , Server.MapPath("~/DriverImages"));
                    }


                    return Json(new { success = true, message = "تم تعديل السائق بنجاح" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<JsonResult> DeleteDeliveryMan(int Id)
        {
            var entity = await context.DeliveryMen.FindAsync(Id);
            context.DeliveryMen.Remove(entity);
            await context.SaveChangesAsync();
            return Json(new { success = true, message = "تم حذف السائق بنجاح" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ToggleDeliveryManStatus(int Id)
        {
            var DeliveryMan = context.DeliveryMen.Find(Id);
            // DeliveryMan.Status = DeliveryMan.Status.Next();
            if (DeliveryMan.Status == Models.Enums.EnumDeliveryManStatus.Offline)
            {
                DeliveryMan.Status = Models.Enums.EnumDeliveryManStatus.Online;
            }
            else if (DeliveryMan.Status == Models.Enums.EnumDeliveryManStatus.Online)
            {
                DeliveryMan.Status = Models.Enums.EnumDeliveryManStatus.Offline;
            }
            context.SaveChanges();
            return Json(new { success = true, message = "تم تغيير الحالة" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ToggleDeliveryManActive(int Id)
        {
            var DeliveryMan = context.DeliveryMen.Find(Id);
            DeliveryMan.isActive = !DeliveryMan.isActive;
            context.SaveChanges();
            return Json(new { success = true, message = "تم تغيير التفعيل" }, JsonRequestBehavior.AllowGet);
        }
    }
}