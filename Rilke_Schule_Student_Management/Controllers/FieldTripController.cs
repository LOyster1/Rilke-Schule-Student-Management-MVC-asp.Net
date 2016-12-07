using Microsoft.AspNet.Identity;
using Rilke_Schule_Student_Management.Models;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;

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
            IEnumerable<SelectListItem> teacherId = new SelectList(db.Teachers.ToList(), "Teacher_Id", "Teacher_Id");
            ViewData["teacherId"] = teacherId;

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
        public ActionResult SelectFieldTrip()
        {
            return View(db.FieldTrips.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewAllTrips(int id)
        {
            var sign_up = from m in db.SignUps
                          where m.FieldTrip_Id == id
                          select m;

            return View(sign_up.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditTripDetails(int id)
        {
            //Send to the view, the userTypeList
            IEnumerable<SelectListItem> teacherId = new SelectList(db.Teachers.ToList(), "Teacher_Id", "Teacher_Id");
            ViewData["teacherId"] = teacherId;

            ViewBag.DepartureTime = db.FieldTrips.Find(id).DepartureTime.Value.ToShortTimeString();
            ViewBag.ReturnTime = db.FieldTrips.Find(id).ReturnTime.Value.ToShortTimeString();
            ViewBag.ChapperoneArrivalTime = db.FieldTrips.Find(id).ChapperoneArrivalTime.Value.ToShortTimeString();

            return View(db.FieldTrips.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmEditTripDetails(FieldTrip ft)
        {
            var update = db.FieldTrips.Find(ft.FieldTrip_Id);
            update.Teacher_Id = ft.Teacher_Id;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewPermissionSlip()
        {
            int tripId = Request.Form["fieldTripId"].AsInt();
            int studentId = Request.Form["studentId"].AsInt();
            string teacherId = db.FieldTrips.Find(tripId).Teacher_Id;

            var classIdList = from c in db.Classes
                              where c.Student_Number == studentId && teacherId.Equals(c.Teacher_Id)
                              select c.Class_Id;

            int classId = classIdList.First();

            ViewBag.trip = db.FieldTrips.Find(tripId);
            ViewBag.SubmitByDate = db.FieldTrips.Find(tripId).SubmitByDate.Value.ToLongDateString();
            ViewBag.TripDate = db.FieldTrips.Find(tripId).TripDate.Value.Date.ToLongDateString();
            ViewBag.DepartureTime = db.FieldTrips.Find(tripId).DepartureTime.Value.ToShortTimeString();
            ViewBag.ChaperoneArrivalTime = db.FieldTrips.Find(tripId).ChapperoneArrivalTime.Value.ToShortTimeString();
            ViewBag.ChaperoneCost = db.FieldTrips.Find(tripId).ChapperoneCost;
            ViewBag.ReturnTime = db.FieldTrips.Find(tripId).ReturnTime.Value.ToShortTimeString();
            ViewBag.tripId = tripId;
            ViewBag.classId = classId;
            ViewBag.studentName = db.Students.Find(studentId).Stud_F_Name + " " + db.Students.Find(studentId).Stud_L_Name;
            ViewBag.UserId = User.Identity.GetUserId();

            return View();
        }

        [Authorize(Roles = "Parent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitPermissionSlip(SignUp model)
        {
            string message = "Trip Not Added";
           
            try
            {
                db.SignUps.Add(model);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("FieldTripmanager", "FieldTrip");
                }
            }
            catch (DbEntityValidationException e)
            {

            }
            return View(message);
        }
        
        [Authorize(Roles = "Parent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewTrip()
        {
            int studentId = Request.Form["studentid"].AsInt();

            // Queryable list of classes with a matching student_number matching the student numbers from the guardian variable.
            var classes = from model in db.Classes
                          where model.Student_Number.Equals(studentId)
                          select model.Teacher_Id;

            var fieldTrips = from m in db.FieldTrips
                           where classes.Contains(m.Teacher_Id)
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