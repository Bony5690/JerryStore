using System.Web;
using System.ComponentModel.DataAnnotations;
using System;

namespace JerryStore.Models
{
    public class Student
    {
        [Display(Name= "ID")]
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Birth Of Date")]
        public DateTime? DateOfBirth { get; set; }
    }
}