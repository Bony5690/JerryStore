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

            using (Models.CustomersEntities entities = new Models.CustomersEntities())
            {
                var package = entities.Packages.Select(x => new PackageModel { id = x.ID, Name = x.Name, Price = x.Price, Assistant = x.Assistant, Task = x.Task}).ToArray();
                return View(package);
            }
        }
        [HttpPost]
        public ActionResult Index(PackageModel model)
        {
            using (Models.CustomersEntities entities = new Models.CustomersEntities())
            {

                Customer dummyCustomer = null;
                dummyCustomer = entities.Customers.FirstOrDefault(x => x.FirstName == "Dummy" && x.LastName == "Customer");
                if(dummyCustomer == null)
                {
                    dummyCustomer = new Customer { FirstName = "Dummy", LastName = "Customer" };
                    entities.Customers.Add(dummyCustomer);
                }
                dummyCustomer.CustomerPackages.Add(new CustomerPackage { PackageID = model.id });

                entities.SaveChanges();
                return RedirectToAction("Index", "checkout");
            }
            
        }



    }
; }