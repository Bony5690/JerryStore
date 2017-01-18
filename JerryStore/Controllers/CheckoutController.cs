using JerryStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JerryStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            ViewBag.HeaderId = "back";
            ViewBag.HeaderClass = "hide";
            CheckoutModel model = new CheckoutModel();

            string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
            string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantId"];


            Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);

            using (CustomersEntities entities = new CustomersEntities())
            {
                var currentCustomer = entities.Customers.Single(x => x.EmailAddress == User.Identity.Name);
                var currentPackage = currentCustomer.CustomerPackages.First(x => x.PurchaseDate == null);
                
                model.Package = new PackageModel();
                model.Package.Name = currentPackage.Package.Name;
                model.Package.Price = currentPackage.Package.Price;
                model.Package.Assistant = currentPackage.Package.Assistant;
                model.Package.Task = currentPackage.Package.Task;
                model.FirstName = currentCustomer.FirstName;
                model.LastName = currentCustomer.LastName;
                model.EmailAddress = currentCustomer.EmailAddress.Trim();
               
            }

                return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckoutModel model)
        {
            string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
            string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantId"];


            Braintree.BraintreeGateway braintree = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);
            //int userId = -1;
            if (ModelState.IsValid)
            {
                using (CustomersEntities entities = new CustomersEntities())
                {
                    var currentCustomer = entities.Customers.Single(x => x.EmailAddress == User.Identity.Name);
                    var currentPackage = currentCustomer.CustomerPackages.First(x => x.PurchaseDate == null);


                    //TODO: Validate the credit card - if it errors out, add a model error and display it to the user
                    //TODO: Persist this information to the database

                    //var currentPackage = entities.Customers.Single(x => x.EmailAddress == User.Identity.Name).CustomerPackages.First(x => x.PurchaseDate == null);
                    //model.Package = new PackageModel();
                    //string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                    //string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                    //string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
                    //string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantId"];


                    Braintree.CustomerRequest request = new Braintree.CustomerRequest();
                    request.Email = model.EmailAddress;
                    request.FirstName = model.FirstName;
                    request.LastName = model.LastName;
                    request.Phone = model.PhoneNumber;
                    request.CreditCard = new Braintree.CreditCardRequest();

                    request.CreditCard.Number = model.CreditCardNumber;
                    request.CreditCard.CardholderName = model.CreditCardName;
                    request.CreditCard.ExpirationMonth = (model.CreditCardExpirationMonth).ToString().PadLeft(2, '0');
                    request.CreditCard.ExpirationYear = model.CreditCardExpirationYear.ToString();


                    var customerResult = braintree.Customer.Create(request);
                    Braintree.TransactionRequest sale = new Braintree.TransactionRequest();
                    sale.Amount = currentPackage.Package.Price;
    
                        
                    sale.CustomerId = customerResult.Target.Id;
                    sale.PaymentMethodToken = customerResult.Target.DefaultPaymentMethod.Token;
                    braintree.Transaction.Sale(sale);

                    currentPackage.PurchaseDate = DateTime.UtcNow;
                    entities.SaveChanges();

                    return RedirectToAction("Receipt", "Membership", null);
                }
            }
            return View(model);
        }   
    }
}