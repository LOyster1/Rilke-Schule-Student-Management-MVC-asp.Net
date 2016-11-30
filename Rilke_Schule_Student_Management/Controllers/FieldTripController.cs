using Rilke_Schule_Student_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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

        //------------------------- Admin -------------------------//

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
                if (result > 0)
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
            update.ChapperoneCost = ft.ChapperoneCost;
            update.DepartureTime = ft.DepartureTime;
            update.ReturnTime = ft.ReturnTime;
            update.Transportation = ft.Transportation;

            db.SaveChanges();

            return RedirectToAction("EditTrip", "FieldTrip");
        }

        //------------------------- Parent -------------------------//

        [Authorize(Roles = "Parent")]
        public ActionResult SelectStudent()
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
        public ActionResult ViewPermissionSlip(int tripId, int studentId)
        {
            ViewBag.trip = db.FieldTrips.Find(tripId);
            ViewBag.SubmitByDate = db.FieldTrips.Find(tripId).SubmitByDate.Value.ToLongDateString();
            ViewBag.TripDate = db.FieldTrips.Find(tripId).TripDate.Value.Date.ToLongDateString();
            ViewBag.DepartureTime = db.FieldTrips.Find(tripId).DepartureTime.Value.ToShortTimeString();
            ViewBag.ChaperoneArrivalTime = db.FieldTrips.Find(tripId).ChapperoneArrivalTime.Value.ToShortTimeString();
            ViewBag.ChaperoneCost = db.FieldTrips.Find(tripId).ChapperoneCost;
            ViewBag.ReturnTime = db.FieldTrips.Find(tripId).ReturnTime.Value.ToShortTimeString();
            ViewBag.tripId = tripId;
            ViewBag.studentId = studentId;
            ViewBag.studentName = db.Students.Find(studentId).Stud_F_Name + " " + db.Students.Find(studentId).Stud_L_Name;
            ViewBag.UserId = User.Identity.GetUserId();

            return View();
        }

        [Authorize(Roles = "Parent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewPermissionSlip(SignUp model)
        {
            string message = "Trip Not Added";
           
            try
            {
                db.SignUps.Add(model);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("ViewTrip", "FieldTrip", new { studentId = model.Student_Number });
                }
            }
            catch (DbEntityValidationException e)
            {

            }
            return View(message);
        }
        
        [Authorize(Roles = "Parent")]
        public ActionResult ViewTrip(int studentId)
        {            
            // Queryable list of classes with a matching student_number matching the student numbers from the guardian variable.
            var classes = from model in db.Classes
                          where model.Student_Number.Equals(studentId)
                          select model.Class_Id;

            var fieldTrips = from m in db.FieldTrips
                           where classes.Contains(m.Class_Id)
                           select m;
            ViewBag.studentId = studentId;
            return View(fieldTrips.ToList());
        }

        //    [Authorize(Roles = "Parent")]
        //    public ActionResult ViewTrip()
        //    {
        //        var FieldTripIds = new List<int>();
        //        var userId = User.Identity.GetUserId();

        //        // Queryable list of Guardianships with a matching username and to the user in session.
        //        var guardian = db.Guardianships.Where(i => i.UserName == userId);

        //        // looping thorugh the guardianships with the matching usernames
        //        foreach (var item in guardian)
        //        {
        //            // Queryable list of classes with a matching student_number matching the student numbers from the guardian variable.
        //            var classes = db.Classes.Where(i => i.Student_Number == item.Student_Number);

        //            // Looping through the classes
        //            foreach (var i in classes)
        //            {
        //                // Finding the FieldTrips associated with the classes
        //                var trips = from m in db.FieldTrips
        //                            where m.Class_Id == i.Class_Id
        //                            select m;
        //                // Adding each FieldTrip_Id to the FieldTripIds list
        //                foreach (var t in trips)
        //                {
        //                    FieldTripIds.Add(t.FieldTrip_Id);
        //                }
        //            }
        //            //studentFieldtrips.Add(FieldTripIds);
        //        }
        //        var trip = from m in db.FieldTrips
        //                   where FieldTripIds.Contains(m.FieldTrip_Id)
        //                   select m;

        //        return View(trip.ToList());
        //    }
    }
}