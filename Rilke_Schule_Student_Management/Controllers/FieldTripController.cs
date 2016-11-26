using Rilke_Schule_Student_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
            if (ModelState.IsValid)
            {
                FieldTrip FieldTripEntity = new FieldTrip
                {
                    TripName = model.TripName,
                    SubmitByDate = model.SubmitByDate,
                    TripDate = model.TripDate,
                    ChapperoneArrivalTime = model.ChapperoneArrivalTime,
                    DepartureTime = model.DepartureTime,
                    ReturnTime = model.ReturnTime,
                    Transportation = model.Transportation
                };
                
                using (db)
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
                }
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
        public ActionResult ViewPermissionSlip()
        {
            return View();
        }

        [Authorize(Roles = "Parent")]
        public ActionResult ViewTrip()
        {
            return View(db.FieldTrips.ToList());
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