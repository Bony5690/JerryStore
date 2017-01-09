using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JerryStore.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            Models.RegistrationModel model = new Models.RegistrationModel();
            return View(model);
        }

        // Post: Registration
        [HttpPost]
        public ActionResult Index(Models.RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
               if (WebMatrix.WebData.WebSecurity.UserExists(model.EmailAddress))
                {
                    ModelState.AddModelError("EmailAddress", "Username already Exist");
                }
               else
                {
                    WebMatrix.WebData.WebSecurity.CreateUserAndAccount(model.EmailAddress, model.Password, null, false); 
                }
            }

            return View(model);
        }
    }
}