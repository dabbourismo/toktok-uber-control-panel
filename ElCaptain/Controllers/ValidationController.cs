using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCaptain.Controllers
{
    public class ValidationController : Controller
    {
        private readonly AppDbContext context;
        public ValidationController()
        {
            context = new AppDbContext();
        }
        [HttpGet]
        public JsonResult CheckUserNameDublicate(string Username, string PreviousUsername)
        {
            if (Username == PreviousUsername)
            {
                return Json(true, JsonRequestBehavior.AllowGet); ;
            }
            bool isExist = context.Clients.FirstOrDefault(x => x.Username == Username) != null;
            if (!isExist)
            {
                Response.AddHeader("username", "متاح");
            }
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }

    }
}