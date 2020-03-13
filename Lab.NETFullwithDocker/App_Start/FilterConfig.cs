using System.Web;
using System.Web.Mvc;

namespace Lab.NETFullwithDocker
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
