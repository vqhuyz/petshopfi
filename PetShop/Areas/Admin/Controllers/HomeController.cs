using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        PetShopDataContext db = new PetShopDataContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if(Session["AdminLogin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Thang = DateTime.Now.Month;
            return View();
        }

        public ActionResult GetTarget()
        {
            decimal? target  = 0;
            List<Target> lstTargets = new List<Target>();
            var dt = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            for (int i = 1; i<= dt; i++)
            {
                target = db.Orders.Where(x => x.CreatedDate.Value.Day == i && x.CreatedDate.Value.Month == DateTime.Now.Month).Sum(x=>x.TotalMoney);
                lstTargets.Add(new Target(i.ToString(), target));
            }
            return Json(lstTargets, JsonRequestBehavior.AllowGet);
        }
    }
}