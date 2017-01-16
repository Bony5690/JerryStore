using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JerryStore.Models
{
    public class ReceiptModel
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PackageName { get; set; }
        public string Assistant { get; set; }
        public int Task { get; set; }
        public decimal Price { get; set; }
        public string EmailAddress { get; internal set; }
    }
}