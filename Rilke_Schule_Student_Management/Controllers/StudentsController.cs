using Microsoft.AspNet.Identity;
using Rilke_Schule_Student_Management.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;

namespace Rilke_Schule_Student_Management.Controllers
{
    public class StudentsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Students
        public StudentsController()
        {
            db = new ApplicationDbContext();
        }
        
        [Authorize(Roles = "Parent")]
        public ActionResult AddStudent()
        {
            return View();
        }

        [Authorize(Roles = "Parent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent(Student viewModel)
        //Get entered Student information and query it against Students, if student exists in Students, add Guardianship entity
        {
            List<int> StudentList = new List<int>();
            var parent = User.Identity.GetUserId();
            
            var F_Name = viewModel.Stud_F_Name;
            var L_Name = viewModel.Stud_L_Name;
            var DOB = viewModel.Date_Of_Birth;
            bool added = false;


            var existingStudent = from m in db.Students
                                  where m.Stud_F_Name == F_Name && m.Stud_L_Name == L_Name
                                  select m;

            var dateCheck = from m in existingStudent
                            where DOB.Equals(m.Date_Of_Birth)
                            select m.Student_Number;

            foreach (int s in dateCheck)
            {
                Guardianship guardianship = new Guardianship();
                guardianship.UserName = parent;
                guardianship.Student_Number = s;
                db.Guardianships.Add(guardianship);
                added = true;
            }

            db.SaveChanges();
            if (!added)
            {
                return RedirectToAction("AddStudent", "Students");
            }
            


            return RedirectToAction("ManageStudent", "Students");
        }
        [Authorize(Roles = "Parent")]
        public ActionResult ManageStudent()
        {
            return View();
        }
        [Authorize(Roles = "Parent")]
        public ActionResult AddGuardianship()
        {

            return View();
        }
    }
}