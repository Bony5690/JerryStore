﻿using JerryStore.Models;
using SendGrid.Helpers.Mail;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace JerryStore.Controllers
{
    public class MembershipController : Controller
    {
        
        
            // GET: Login
            public ActionResult Login()
            {
            ViewBag.HeaderId = "back";
            ViewBag.HeaderClass = "hide";
            LoginModel model = new LoginModel();
                return View(model);
            }

            // POST: Login

            [HttpPost]
            public ActionResult Login(Models.LoginModel model)
            {
                if (ModelState.IsValid)
                {
                    if (WebSecurity.Login(model.EmailAddress, model.Password, model.PersistCookie))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("EmailAddress", "Username or password was incorrect.");

                }
                return View(model);
            }

            // GET: Logout
            public ActionResult Logout()
            {
                WebSecurity.Logout();
                return RedirectToAction("Index", "Home");
            }

            // GET: Registration
            public ActionResult Registration()
            {
            ViewBag.HeaderId = "back";
            ViewBag.HeaderClass = "hide";
            RegistrationModel model = new RegistrationModel();
                return View(model);
            }

            // POST: Registration
            [HttpPost]
            public async Task<ActionResult> Registration(RegistrationModel model)
            {
                if (ModelState.IsValid)
                {
                    if (WebSecurity.UserExists(model.EmailAddress))
                    {
                        ModelState.AddModelError("EmailAddress", "Username already in user");
                    }
                    else
                    {
                        string confirmationToken = WebSecurity.CreateUserAndAccount(model.EmailAddress, model.Password, new { FirstName = model.FirstName, LastName = model.LastName }, true);
                        string confirmationUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Membership/Confirm?confirmationToken=" + confirmationToken;

                        string sendGridApiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                        SendGrid.SendGridAPIClient client = new SendGrid.SendGridAPIClient(sendGridApiKey);

                        Email from = new Email("info@leaveittous.com");
                        string subject = "Confirm your new account";
                        Email to = new Email(model.EmailAddress);
                        Content content = new Content("text/html", string.Format("<a href=\"{0}\">Confirm</a>", confirmationUrl));

                        Mail mail = new Mail(from, subject, to, content);
                        mail.TemplateId = "ae484be0-14bc-45f2-a56c-6fa9c6704e31";
                        mail.Personalization[0].AddSubstitution("-link-", confirmationUrl);
                        var response = await client.client.mail.send.post(requestBody: mail.Get());

                        string message = await response.Body.ReadAsStringAsync();
                        return RedirectToAction("ConfirmationSent");
                    }
                }
                return View(model);
            }

            // GET: ForgotPassword
            public ActionResult ForgotPassword()
            {
                var model = new ForgotPasswordModel();
                return View(model);
            }

        // GET: Receipt
        public ActionResult Receipt()
        {
            using (Models.CustomersEntities entities = new Models.CustomersEntities())
            {
                ViewBag.HeaderId = "back";
                ViewBag.HeaderClass = "hide";
                ReceiptModel model = new ReceiptModel();
                var currentCustomer = entities.Customers.Single(x => x.EmailAddress == User.Identity.Name);
                var currentPackage = currentCustomer.CustomerPackages.First(x => x.PurchaseDate != null);

               
                model.FirstName = currentPackage.Package.Name;
                model.Price = currentPackage.Package.Price;
                model.Assistant = currentPackage.Package.Assistant;
                model.Task = currentPackage.Package.Task;
                model.FirstName = currentCustomer.FirstName;
                model.LastName = currentCustomer.LastName;
                model.EmailAddress = currentCustomer.EmailAddress.Trim();
               
                return View(model);
            }   
           
        }



        [HttpPost]
            public async Task<ActionResult> ForgotPassword(Models.ForgotPasswordModel model)
            {
                if (WebSecurity.UserExists(model.EmailAddress))
                {
                    string resetToken = WebSecurity.GeneratePasswordResetToken(model.EmailAddress);
                    string resetUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Membership/Reset?resetToken=" + resetToken;

                    string sendGridApiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                    SendGrid.SendGridAPIClient client = new SendGrid.SendGridAPIClient(sendGridApiKey);

                    Email from = new Email("admin@codingtemple.com");
                    string subject = "Instructions for Resetting Your Password";
                    Email to = new Email(model.EmailAddress);
                    Content content = new Content("text/html", string.Format("<a href=\"{0}\">Reset your password</a>", resetUrl));
                    Mail mail = new Mail(from, subject, to, content);

                    var response = await client.client.mail.send.post(requestBody: mail.Get());

                    string message = await response.Body.ReadAsStringAsync();
                }

                return RedirectToAction("ResetSent");
            }

            public ActionResult ResetSent()
            {
                return View();
            }

            public ActionResult Reset(string resetToken)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetToken = resetToken;
                return View(model);
            }

            [HttpPost]
            public ActionResult Reset(ResetPasswordModel model)
            {
                if (ModelState.IsValid)
                {
                    WebSecurity.ResetPassword(model.ResetToken, model.Password);
                    TempData["PasswordChanged"] = true;
                    return RedirectToAction("Login");

                }
                return View(model);
            }

            public ActionResult ConfirmationSent()
            {
                return View();
            }

            public ActionResult Confirm(string confirmationToken)
            {
                if (WebSecurity.ConfirmAccount(confirmationToken))
                {
                    TempData["AccountConfirmed"] = true;
                    return RedirectToAction("Login");
                }
                return RedirectToAction("Index", "Home");
            }
        }
    }