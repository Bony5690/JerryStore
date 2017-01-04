using JerryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JerryStore.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Students/
        public ActionResult Index(int? ID)
        {

            using (Models.CustomersEntities entities = new Models.CustomersEntities())
            {
                var package = entities.Packages.Single(x => x.ID == ID);
                ServiceModel model = new ServiceModel();
                model.id = package.ID;
                model.Name = package.Name;
                model.Price = package.Price;
                return View(model);
            }
            
          //      return View(new Service { ID = 1, Package = "Starter", Task = "5", Assistant = "Random Assistant", Price = 19.95m });

                //services.Add(new Service { ID = 2, Package = "Professional", Task = "25", Assistant = "Dedicated Assistant", Price = 19.95 });

               // services.Add(new Service { ID = 3, Package = "Executive", Task = "50", Assistant = "Random Assistant", Price = 179.95 });//

            
        }
        // POST: Students/Edit/{id}
        [HttpPost]
        public ActionResult Index(Service service)
        {
            return RedirectToAction("Index", "checkout");
        }



    }
}