using JerryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JerryStore.Controllers
{
    public class StudentsController : Controller
    {
        private static List<Student> students = new List<Student>();
        // GET: Studentsif/{}
        public ActionResult Index(int? id)
        {
            if(students.Count == 0)
            {
             students.Add(new Student { ID = 1, FirstName = "Jerry", LastName ="Bony" });

           students.Add(new Student { ID = 2, FirstName = "Jimmy", LastName = "Ellis" });

           students.Add(new Student { ID = 3, FirstName = "Jin", LastName = "Xiao" });

           students.Add(new Student { ID = 4, FirstName = "Serk", LastName = "Bony" });

           students.Add(new Student { ID = 5, FirstName = "Tessa", LastName = "" });
            }

            if(id.HasValue && students.Any(x => x.ID == id.Value))
            {
                return View(students.Single(x => x.ID == id.Value));
            }

            if(id != null) 
            {
                switch (id.Value)
                {
                    case 1:
                        return Json(new { name = "Jerry Bony" }, JsonRequestBehavior.AllowGet);
                    case 2:
                        return Json(new { name = "Jin Xia" }, JsonRequestBehavior.AllowGet);
                    case 3:
                        return Json(new { name = "Tessa Konkol" }, JsonRequestBehavior.AllowGet);
                    case 4:
                        return Json(new { name = "Serchan Nizam " }, JsonRequestBehavior.AllowGet);
                    case 5:
                        return Json(new { name = "Jimmy ellis" }, JsonRequestBehavior.AllowGet);
                }
            }
            return HttpNotFound("No Student Found");
           
    }
        //Post request
        [HttpPost]
        public ActionResult Index(int id, Student student)
        {
            Student s = students.First(x => x.ID == id);
            s.DateOfBirth = student.DateOfBirth;
            s.FirstName = student.FirstName;
            s.LastName = student.LastName;
            return View(s);
        }
    }
    

    
}