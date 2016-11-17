using System.Web.Mvc;

namespace Rilke_Schule_Student_Management.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Parent")]
        public ActionResult Activity()
        {
            return View();
        }
    }
}