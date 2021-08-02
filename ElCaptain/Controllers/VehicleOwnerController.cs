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
using ElCaptain.Services;

namespace ElCaptain.Controllers
{
    public class VehicleOwnerController : Controller
    {
        private readonly AppDbContext context;

        public VehicleOwnerController()
        {
            context = new AppDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ViewAllVehicleOwners()
        {
            //Server side parameters
            List<VehicleOwnerDto> VehicleOwnersList;
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
                VehicleOwnersList = await context.VehicleOwners
                    .Where(x => x.Name.Contains(searchValue)
                    || (x.Phone.Contains(searchValue))
                    || (x.Password.Contains(searchValue))
                    || (x.NationalId.Contains(searchValue)))
                    .ProjectTo<VehicleOwnerDto>().ToListAsync();
                totalRows = VehicleOwnersList.Count;
                totalFilteredRows = VehicleOwnersList.Count;
            }
            else
            {
                //Paging
                VehicleOwnersList = await context.VehicleOwners.OrderBy(x => x.Id).Skip(start).Take(length).ProjectTo<VehicleOwnerDto>().ToListAsync();
                totalRows = context.VehicleOwners.Count();
                totalFilteredRows = context.VehicleOwners.Count();
            }

            //Sorting
            VehicleOwnersList = VehicleOwnersList.OrderBy(sortColumnName + " " + sortDirection).ToList();

            foreach (var item in VehicleOwnersList)
            {
                item.NumberOfVehicles = context.DeliveryMen.Where(x => x.VehicleOwnerId == item.Id).Select(x => x.Id).Count();
            }

            return Json(new { data = VehicleOwnersList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<ActionResult> AddEditVehicleOwner(int id = 0)
        {
            switch (id)
            {
                case 0:
                    return View(new VehicleOwnerDto());
                default:
                    var VehicleOwner = await context.VehicleOwners.FirstOrDefaultAsync(x => x.Id == id);
                    var dto = Mapper.Map<VehicleOwner, VehicleOwnerDto>(VehicleOwner);
                    return View(dto);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditVehicleOwner(VehicleOwnerDto VehicleOwnerDto)
        {
            var VehicleOwner = Mapper.Map<VehicleOwnerDto, VehicleOwner>(VehicleOwnerDto);
            //add operation
            switch (VehicleOwnerDto.Id)
            {
                case 0:
                    context.VehicleOwners.Add(VehicleOwner);
                    await context.SaveChangesAsync();
                    if (VehicleOwnerDto.ImageUpload != null)
                    {
                        FileUploader.UploadFTPFile(VehicleOwnerDto.ImageUpload.FileName
                            , VehicleOwner.Id
                            , VehicleOwnerDto.ImageUpload
                            , Server.MapPath("~/OwnerNationalIdImages"));
                    }

                    if (VehicleOwnerDto.ImageUpload2 != null)
                    {
                        FileUploader.UploadFTPFile(VehicleOwnerDto.ImageUpload2.FileName
                            , VehicleOwner.Id
                            , VehicleOwnerDto.ImageUpload2
                            , Server.MapPath("~/VehicleOwnerContract"));
                    }
                    return Json(new { success = true, message = "تم اضافة مالك المركبة بنجاح" }, JsonRequestBehavior.AllowGet);
                default:
                    context.Entry(VehicleOwner).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    if (VehicleOwnerDto.ImageUpload != null)
                    {
                        FileUploader.DeleteFTPFile(VehicleOwner.Id, Server.MapPath("~/OwnerNationalIdImages"));
                        FileUploader.UploadFTPFile(VehicleOwnerDto.ImageUpload.FileName
                            , VehicleOwner.Id
                            , VehicleOwnerDto.ImageUpload
                            , Server.MapPath("~/OwnerNationalIdImages"));
                    }

                    if (VehicleOwnerDto.ImageUpload2 != null)
                    {
                        FileUploader.DeleteFTPFile(VehicleOwner.Id, Server.MapPath("~/VehicleOwnerContract"));
                        FileUploader.UploadFTPFile(VehicleOwnerDto.ImageUpload2.FileName
                            , VehicleOwner.Id
                            , VehicleOwnerDto.ImageUpload2
                            , Server.MapPath("~/VehicleOwnerContract"));
                    }

                    return Json(new { success = true, message = "تم تعديل مالك المركبة بنجاح" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<JsonResult> DeleteVehicleOwner(int Id)
        {
            var entity = await context.VehicleOwners.FindAsync(Id);
            context.VehicleOwners.Remove(entity);
            await context.SaveChangesAsync();
            return Json(new { success = true, message = "تم حذف مالك المركبة بنجاح" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ToggleVehicleOwnerActive(int Id)
        {
            var VehicleOwner = context.VehicleOwners.Find(Id);
            VehicleOwner.isActive = !VehicleOwner.isActive;
            context.SaveChanges();
            return Json(new { success = true, message = "تم تغيير التفعيل" }, JsonRequestBehavior.AllowGet);
        }
    }
}