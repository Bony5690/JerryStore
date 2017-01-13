using JerryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JerryStore.Controllers
{
    
    public class PackagesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.HeaderId = "Pricing";
            using (Models.CustomersEntities entities = new Models.CustomersEntities())
            {
                var package = entities.Packages.Select(x => new PackageModel { id = x.ID, Name = x.Name, Price = x.Price, Assistant = x.Assistant, Task = x.Task}).ToArray();
                return View(package);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(PackageModel model)
        {
            using (Models.CustomersEntities entities = new Models.CustomersEntities())
            {
                
                Customer customer = entities.Customers.FirstOrDefault(x => x.EmailAddress == User.Identity.Name);
                
                customer.CustomerPackages.Add(new CustomerPackage { PackageID = model.id });

                entities.SaveChanges();
                return RedirectToAction("Index", "checkout");
            }
            
        }



    }
; }