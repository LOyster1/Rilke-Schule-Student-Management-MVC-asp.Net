using System.Web;
using System.Web.Mvc;

namespace Rilke_Schule_Student_Management
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
