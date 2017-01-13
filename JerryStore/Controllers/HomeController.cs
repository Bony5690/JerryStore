using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JerryStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.HeaderId = "image";
            ViewBag.HeaderClass = "parallax";
            return View();
        }
        
        
        public ActionResult AboutUs()
        {
            ViewBag.HeaderId = "other-color";
            return View();
        }



        public ActionResult TaskIdeas()
        {
            ViewBag.HeaderId = "task-picture";
            return View();
        }


        public ActionResult  PlansandPricing()
        {
            ViewBag.HeaderId = "Pricing";
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.HeaderId = "Pricing";
            return View();
        }

        


    }
}