using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JerryStore.Models;

namespace JerryStore.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private CustomersEntities db = new CustomersEntities();

        // GET: Tasks
        public ActionResult Index()
        {
            var customerPackageTasks = db.CustomerPackageTasks.Where(x => x.CustomerPackage.Customer.EmailAddress == User.Identity.Name).Include(c => c.CustomerPackage);
            return View(customerPackageTasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerPackageTask customerPackageTask = db.CustomerPackageTasks.Single(x => x.Id == id && x.CustomerPackage.Customer.EmailAddress == User.Identity.Name);
            if (customerPackageTask == null)
            {
                return HttpNotFound();
            }
            return View(customerPackageTask);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerPackageTask customerPackageTask)
        {
            if (ModelState.IsValid)
            {
                var currentUser = db.Customers.Single(x => x.EmailAddress == User.Identity.Name);
                var currentPlan = currentUser.CustomerPackages.First();
                //customerPackageTask.CustomerID = db.Customers.Single(x => x.EmailAddress == User.Identity.Name).ID;
                currentPlan.CustomerPackageTasks.Add(customerPackageTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.CustomerPackages, "CustomerID", "CustomerID", customerPackageTask.CustomerID);
            return View(customerPackageTask);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerPackageTask customerPackageTask = db.CustomerPackageTasks.Single(x => x.Id == id && x.CustomerPackage.Customer.EmailAddress == User.Identity.Name);
            if (customerPackageTask == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CustomerID = new SelectList(db.CustomerPackages, "CustomerID", "CustomerID", customerPackageTask.CustomerID);
            return View(customerPackageTask);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerID,PackageID")] CustomerPackageTask customerPackageTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerPackageTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CustomerID = new SelectList(db.CustomerPackages, "CustomerID", "CustomerID", customerPackageTask.CustomerID);
            return View(customerPackageTask);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerPackageTask customerPackageTask = db.CustomerPackageTasks.Single(x => x.Id == id && x.CustomerPackage.Customer.EmailAddress == User.Identity.Name);
            if (customerPackageTask == null)
            {
                return HttpNotFound();
            }
            return View(customerPackageTask);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerPackageTask customerPackageTask = db.CustomerPackageTasks.Single(x => x.Id == id && x.CustomerPackage.Customer.EmailAddress == User.Identity.Name);
            db.CustomerPackageTasks.Remove(customerPackageTask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
