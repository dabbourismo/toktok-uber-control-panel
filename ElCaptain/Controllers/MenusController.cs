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
    public class MenusController : Controller
    {
        private readonly AppDbContext context;
        private DropDownLists dropDownLists;
        public MenusController()
        {
            context = new AppDbContext();
            dropDownLists = new DropDownLists();
        }

        // GET: Menus
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> ViewAllMenus()
        {
            //Server side parameters
            List<MenuDto> menusList;
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
                menusList = await context.Menus
                    .Where(x => x.Title.Contains(searchValue)
                                || x.Store.Name.Contains(searchValue))
                    .ProjectTo<MenuDto>().ToListAsync();
                totalRows = menusList.Count;
                totalFilteredRows = menusList.Count;
            }
            else
            {
                menusList = await context.Menus.ProjectTo<MenuDto>().ToListAsync();
                totalRows = menusList.Count;
                totalFilteredRows = menusList.Count;
            }

            //Sorting
            menusList = menusList.OrderBy(sortColumnName + " " + sortDirection).ToList();
            //Paging
            menusList = menusList.Skip(start).Take(length).ToList();

            return Json(new { data = menusList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalFilteredRows }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public async Task<ActionResult> AddEditMenu(int id = 0)
        {
            switch (id)
            {
                case 0:
                    return View(new MenuDto()
                    {
                        StoresDropDownList = dropDownLists.StoresDropDownList()
                    });
                default:
                    var Menu = await context.Menus.FirstOrDefaultAsync(x => x.Id == id);
                    var dto = Mapper.Map<Menu, MenuDto>(Menu);

                    dto.StoresDropDownList = dropDownLists.StoresDropDownList();
                    SelectListItem selectedStore
                         = dto.StoresDropDownList.Find(e => e.Value == dto.StoreId.ToString());
                    selectedStore.Selected = true;

                    return View(dto);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditMenu(MenuDto MenuDto)
        {
            var menu = Mapper.Map<MenuDto, Menu>(MenuDto);
            //add operation
            switch (MenuDto.Id)
            {
                case 0:
                    context.Menus.Add(menu);
                    await context.SaveChangesAsync();
                    if (MenuDto.ImageUpload != null)
                    {
                        FileUploader.UploadFTPFile(MenuDto.ImageUpload.FileName
                            , menu.Id
                            , MenuDto.ImageUpload
                            , Server.MapPath("~/imgMenus"));
                    }
                    return Json(new { success = true, message = "تم اضافة المنيو بنجاح" }, JsonRequestBehavior.AllowGet);

                default:
                    context.Entry(menu).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    if (MenuDto.ImageUpload != null)
                    {
                        FileUploader.DeleteFTPFile(menu.Id, Server.MapPath("~/imgMenus"));
                        FileUploader.UploadFTPFile(MenuDto.ImageUpload.FileName
                            , menu.Id
                            , MenuDto.ImageUpload
                            , Server.MapPath("~/imgMenus"));
                    }
                    return Json(new { success = true, message = "تم تعديل المنيو بنجاح" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<JsonResult> DeleteMenu(int Id)
        {
            var entity = await context.Menus.FindAsync(Id);
            context.Menus.Remove(entity);
            await context.SaveChangesAsync();

            FileUploader.DeleteFTPFile(Id, Server.MapPath("~/imgMenus"));
            return Json(new { success = true, message = "تم حذف المنيو بنجاح" }, JsonRequestBehavior.AllowGet);
        }
    }
}