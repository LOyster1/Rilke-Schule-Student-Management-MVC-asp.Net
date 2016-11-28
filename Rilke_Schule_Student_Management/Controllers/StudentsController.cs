using Microsoft.AspNet.Identity;
using Rilke_Schule_Student_Management.Models;
using Rilke_Schule_Student_Management.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Rilke_Schule_Student_Management.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: Students
        public StudentsController()
        {
            _context = new ApplicationDbContext();
        }
        
        [Authorize(Roles = "Parent")]
        public ActionResult AddStudent()
        {
            return View();
        }

        [Authorize(Roles = "Parent")]
        [HttpPost]
        public ActionResult AddStudent(StudentFormViewModel viewModel)
        {
            var parent = _context.Users.Single(u => u.Id == User.Identity.GetUserId());

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
            string sql = "SELECT STUDENT_F_NAME * FROM STUDENT WHERE STUDENT";

            return View();
        }
    }
}