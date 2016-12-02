using Microsoft.AspNet.Identity;
using Rilke_Schule_Student_Management.Models;
using System.Collections.Generic;
using System.Data.Entity;
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
            var userId = User.Identity.GetUserId();

            // Queryable list of Guardianships with a matching username and to the user in session.
            var studentId = from m in db.Guardianships
                            where m.Parent.Id == userId
                            select m.Student_Number;

            var students = from s in db.Students
                           where studentId.Contains(s.Student_Number)
                           select s;

            return View(students.ToList());
        }
        [Authorize(Roles = "Parent")]
        public ActionResult DeleteStudent(int id)
        {
            string userId = User.Identity.GetUserId();
            int studentId = id;

            ViewBag.Stud_F_Name = db.Students.Find(id).Stud_F_Name;
            ViewBag.Stud_L_Name = db.Students.Find(id).Stud_L_Name;
            ViewBag.Date_Of_Birth = db.Students.Find(id).Date_Of_Birth.ToShortDateString();

            var guardianshipId = from m in db.Guardianships
                                 where m.UserName == userId && m.Student_Number == studentId
                                 select m;

            if(guardianshipId.Count() > 1)
            {
                return RedirectToAction("DeleteStudent", "Students");
            }
            var output = guardianshipId.First();

            return View(output);
        }
        [Authorize(Roles = "Parent")]
        public ActionResult ConfirmDeleteStudent(int id)
        {
            //Deletes relation between parent & student
            Guardianship delete = db.Guardianships.Find(id);
            if (delete == null)
            {
                return RedirectToAction("ManageStudent", "Students");
            }
            db.Guardianships.Remove(delete);
            db.SaveChanges();

            return RedirectToAction("ManageStudent", "Students");
        }
        [Authorize(Roles = "Parent")]
        public ActionResult AddGuardianship()
        {

            return View();
        }
    }
}