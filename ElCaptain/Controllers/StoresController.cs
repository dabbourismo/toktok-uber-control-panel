using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElCaptain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using ElCaptain.Services;

namespace ElCaptain.Controllers
{
    public class StoresController : Controller
    {
        private readonly AppDbContext context;
        public StoresController()
        {
            context = new AppDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ViewAllStores()
        {
            //Server side parameters
            List<StoreDto> StoresList;
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
                StoresList = await context.Stores
                    .Where(x => x.Name.Contains(searchValue)
                    || (x.Name.Contains(searchValue))
                    || (x.Description.Contains(searchValue))
                    || (x.Address.Contains(searchValue))
                    || (x.Phone1.Contains(searchValue))
                    || (x.Phone2.Contains(searchValue)))
                    .ProjectTo<StoreDto>().ToListAsync();
                totalRows = StoresList.Count;
                totalFilteredRows = StoresList.Count;
            }
            else
            {
                StoresList = await context.Stores.ProjectTo<StoreDto>().ToListAsync();
                totalRows = StoresList.Count;
                totalFilteredRows = StoresList.Count;
            }

            //Sorting
            StoresList = StoresList.OrderBy(sortColumnName + " " + sortDirection).ToList();

            //Paging
            StoresList = StoresList.Skip(start).Take(length).ToList();

            return Json(new { data = StoresList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<ActionResult> AddEditStore(int id = 0)
        {
            switch (id)
            {
                case 0:
                    return View(new StoreDto());
                default:
                    var Store = await context.Stores.FirstOrDefaultAsync(x => x.Id == id);
                    var dto = Mapper.Map<Store, StoreDto>(Store);
                    return View(dto);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditStore(StoreDto StoreDto)
        {
            var store = Mapper.Map<StoreDto, Store>(StoreDto);
            //add operation
            switch (StoreDto.Id)
            {
                case 0:
                    context.Stores.Add(store);
                    context.SaveChanges();
                    if (StoreDto.ImageUpload != null)
                    {
                        FileUploader.UploadFTPFile(StoreDto.ImageUpload.FileName
                            , store.Id
                            , StoreDto.ImageUpload
                            , Server.MapPath("~/imgStores"));
                    }
                    return Json(new { success = true, message = "تم اضافة المتجر بنجاح" }, JsonRequestBehavior.AllowGet);
                default:
                    context.Entry(store).State = EntityState.Modified;
                    context.SaveChanges();

                    if (StoreDto.ImageUpload != null)
                    {
                        FileUploader.DeleteFTPFile(store.Id, Server.MapPath("~/imgStores"));
                        FileUploader.UploadFTPFile(StoreDto.ImageUpload.FileName
                            , store.Id
                            , StoreDto.ImageUpload
                            , Server.MapPath("~/imgStores"));
                    }
                    return Json(new { success = true, message = "تم تعديل المتجر بنجاح" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<JsonResult> DeleteStore(int Id)
        {
            var entity = await context.Stores.FindAsync(Id);
            context.Stores.Remove(entity);
            await context.SaveChangesAsync();
            FileUploader.DeleteFTPFile(Id, Server.MapPath("~/imgStores"));
            return Json(new { success = true, message = "تم حذف المتجر بنجاح" }, JsonRequestBehavior.AllowGet);
        }
    }
}