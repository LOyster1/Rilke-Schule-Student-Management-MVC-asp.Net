using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rilke_Schule_Student_Management.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ActionResult AddStudent()
        {
            return View();
        }
    }
}