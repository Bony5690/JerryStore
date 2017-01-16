using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JerryStore.Models
{
    public class CheckoutModel
    {
        
        
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string EmailAddress { get; set; }

            [Required]
            [Phone]
            public string PhoneNumber { get; set; }

            [Required]
            [CreditCard]
            public string CreditCardNumber { get; set; }

            [Required]
            public string CreditCardName { get; set; }

            [Required]
            //[RegularExpression("/^[0-9]{3,4}$/", ErrorMessage = "Your CVV Is Invalid!")]
            public string CreditCardVerificationValue { get; set; }

            [Required]
            [Range(01, 12)]
            public int CreditCardExpirationMonth { get; set; }

            [Required]
            [Range(2000, 3000)]
            public int CreditCardExpirationYear { get; set; }
        
         public PackageModel Package { get; set; }
    }
    }