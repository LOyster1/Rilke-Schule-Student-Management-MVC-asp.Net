using Microsoft.AspNet.Identity;
using Rilke_Schule_Student_Management.Models;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace Rilke_Schule_Student_Management.Controllers
{
    public class FieldTripController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: FieldTrip
        [Authorize]
        public ActionResult FieldTripManager()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddTrip()
        {
            //Send to the view, the userTypeList
            IEnumerable<SelectListItem> classId = new SelectList(db.Classes.ToList(), "Class_Id", "Class_Id");
            ViewData["classId"] = classId;

            return View();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTrip(FieldTrip model)
        {
            try
            {
                db.FieldTrips.Add(model);
                int result = db.SaveChanges();
                if(result > 0)
                {
                    return RedirectToAction("EditTrip", "FieldTrip");
                }
            }
            catch (DbEntityValidationException e)
            {

            }
            return View("Trip Not Added");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditTrip()
        {
            return View(db.FieldTrips.ToList());
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTrip(int id)
        {
            return View(db.FieldTrips.Find(id));
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmDelete(int id)
        {
            FieldTrip delete = db.FieldTrips.Find(id);
            if (delete == null)
            {
                return RedirectToAction("EditTrip", "FieldTrip");
            }
            db.FieldTrips.Remove(delete);
            db.SaveChanges();

            return RedirectToAction("EditTrip", "FieldTrip");
        }

        [Authorize(Roles = "Parent")]
        public ActionResult ViewPermissionSlip(int id)
        {
            ViewBag.su = new SignUp();
            ViewBag.trip = db.FieldTrips.Find(id);

            return View();
        }

        [Authorize(Roles = "Parent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewPermissionSlip(SignUp model)
        {
            string notAdded = "Trip Not Added";
            model.Student = db.Students.Find(4);
            try
            {
                db.SignUps.Add(model);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("ViewTrip", "FieldTrip");
                }
            }
            catch (DbEntityValidationException e)
            {

            }
            return View(notAdded);
        }

        [Authorize(Roles = "Parent")]
        public ActionResult ViewTrip()
        {
            var FieldTripIds = new List<int>();
            var userId = User.Identity.GetUserId();
            var studentId = db.Guardianships.Where(i => i.UserName == userId);
            foreach(var item in studentId)
            {
                var classes = db.Classes.Where(i => i.Student_Number == item.Student_Number);
                foreach (var i in classes)
                {
                    var trips = from m in db.FieldTrips
                                where m.Class_Id.Equals(i.Class_Id)//---Levi Changed to reflect String
                                select m;
                    foreach(var t in trips)
                    {
                        FieldTripIds.Add(t.FieldTrip_Id);
                    }
                }
            }
            var trip = from m in db.FieldTrips
                        where FieldTripIds.Contains(m.FieldTrip_Id)
                        select m;

            return View(trip.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewAllTrips()
        {
            return View(db.FieldTrips.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditTripDetails(int id)
        {
            //Send to the view, the userTypeList
            IEnumerable<SelectListItem> classId = new SelectList(db.Classes.ToList(), "Class_Id", "Class_Id");
            ViewData["classId"] = classId;

            return View(db.FieldTrips.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmEditTripDetails(FieldTrip ft)
        {
            var update = db.FieldTrips.Find(ft.FieldTrip_Id);
            update.Class_Id = ft.Class_Id;
            update.TripName = ft.TripName;
            update.SubmitByDate = ft.SubmitByDate;
            update.TripDate = ft.TripDate;
            update.ChapperoneArrivalTime = ft.ChapperoneArrivalTime;
            update.DepartureTime = ft.DepartureTime;
            update.ReturnTime = ft.ReturnTime;
            update.Transportation = ft.Transportation;

            db.SaveChanges();

            return RedirectToAction("EditTrip", "FieldTrip");
        }
    }
}