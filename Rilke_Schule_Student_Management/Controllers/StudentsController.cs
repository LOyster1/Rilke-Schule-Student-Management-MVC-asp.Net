using Microsoft.AspNet.Identity;
using Rilke_Schule_Student_Management.Models;
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

            var parent = User.Identity.GetUserId();
            
            var F_Name = viewModel.Stud_F_Name;
            var L_Name = viewModel.Stud_L_Name;
            var DOB = viewModel.Date_Of_Birth;


            var existingStudent = from m in db.Students
                          where m.Stud_F_Name == F_Name && m.Stud_L_Name==L_Name && DbFunctions.DiffDays(m.Date_Of_Birth, DOB) == 0 
                        select m;
            foreach(var t in existingStudent)
            {
                Guardianship guardianship = new Guardianship();
                guardianship.UserName = parent;
                guardianship.Student_Number = t.Student_Number;
                db.Guardianships.Add(guardianship);
                
            }
            db.SaveChanges();
            if (existingStudent == null)
            {

                return RedirectToAction("AddStudent", "Students");
            }
            


            return View();
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