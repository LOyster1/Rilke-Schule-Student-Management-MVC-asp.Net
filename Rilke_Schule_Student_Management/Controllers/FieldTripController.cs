using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rilke_Schule_Student_Management.Controllers
{
    public class FieldTripController : Controller
    {
        // GET: FieldTrip
        [Authorize]
        public ActionResult FieldTripManager()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddTrip()
        {
            return View();
        }
        [Authorize(Roles = "Parent")]
        public ActionResult ViewTrip()
        {
            return View();
        }
    }
}