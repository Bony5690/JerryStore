﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JerryStore.Models
{
    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

    }
    public class ResetPasswordModel
    {

        [Required]
        public string Password { get; set; }
        public string ResetToken { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Keep Me Logged In")]
        public bool PersistCookie { get; set; }
    }
}