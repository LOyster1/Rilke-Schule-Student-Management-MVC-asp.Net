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

                ApplicationDbContext db = new ApplicationDbContext();
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
            ApplicationDbContext db = new ApplicationDbContext();
            return View(db.FieldTrips.ToList());
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTrip(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return View(db.FieldTrips.Find(id));
        }

        // work in progress wont delete
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmDelete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            FieldTrip tempFT = db.FieldTrips.Find(id);
            if(tempFT == null)
            {
                return RedirectToAction("EditTrip");
            }
            db.FieldTrips.Remove(tempFT);
            db.SaveChanges();

            return RedirectToAction("EditTrip");
        }

        [Authorize(Roles = "Parent")]
        public ActionResult ViewPermissionSlip()
        {
            return View();
        }

        [Authorize(Roles = "Parent")]
        public ActionResult ViewTrip()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return View(db.Students.ToList());
        }
    }
}