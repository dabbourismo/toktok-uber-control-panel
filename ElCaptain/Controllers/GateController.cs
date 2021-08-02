using ElCaptain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCaptain.Controllers
{
    public class GateController : Controller
    {
        private readonly AppDbContext context;

        public GateController()
        {
            context = new AppDbContext();

        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(GateDto dto)
        {
            var owner = context.VehicleOwners.Where(x => x.Username == dto.Username && x.Password == dto.Password).FirstOrDefault();


            if (dto.Username == "admin" && dto.Password == "z123")
            {
                dto.Error = "";
                Session["userType"] = "admin";
                Session["userId"] = 0;
                Session["logged"] = "logged";
                return RedirectToAction("Index", "Order");
            }
            else if (owner != null)
            {
                dto.Error = "";
                Session["userType"] = "owner";
                Session["userId"] = owner.Id;
                Session["logged"] = "logged";
                return RedirectToAction("Index", "Order");
            }
            else
            {
                dto.Error = "Incorrect username or password!";
                return View(dto);
            }

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Gate");
        }
    }
}