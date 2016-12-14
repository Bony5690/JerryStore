using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JerryStore.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Package { get; set; }
        public string Task { get; set; }
        public string Assistant { get; set; }
        public decimal Price { get; set; }
    }
}