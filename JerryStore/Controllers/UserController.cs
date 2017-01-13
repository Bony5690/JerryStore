﻿using JerryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JerryStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {

            using (CustomersEntities entities = new CustomersEntities())
            {
                UserModel[] model = entities.Customers.Select(x => new UserModel
                {
                    ID = x.ID,
                    EmailAddress = x.EmailAddress,
                    Roles = x.webpages_Roles.Select(y => y.RoleName)
                }).ToArray();
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            using (CustomersEntities e = new CustomersEntities())
            {

                var x = e.Customers.Single(u => u.ID == id);
                UserModel model = new UserModel
                {
                    ID = x.ID,
                    EmailAddress = x.EmailAddress,
                    Roles = x.webpages_Roles.Select(y => y.RoleName).ToArray(),
                    AvailableRoles = e.webpages_Roles.Select(y => y.RoleName).ToArray()
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            //UserService.AssignUserToRoles(model.ID, model.Roles);

            return RedirectToAction("Index");
        }

    }
}