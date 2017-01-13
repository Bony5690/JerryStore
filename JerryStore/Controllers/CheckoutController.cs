using JerryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JerryStore.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            CheckoutModel model = new CheckoutModel();

            using (CustomersEntities entities = new CustomersEntities())
            {
                var currentPackage = entities.Customers.Single(x => x.EmailAddress == User.Identity.Name).CustomerPackages.First(x => x.PurchaseDate == null);
                model.PackageName = currentPackage.Package.Name;
                model.PackagePrice = currentPackage.Package.Price;
                
            }

                return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Validate the credit card - if it errors out, add a model error and display it to the user
                //TODO: Persist this information to the database
                return RedirectToAction("Index", "Receipt", null);
            }
            return View(model);
        }   
    }
}