using System.Web;
using System.Web.Mvc;

namespace ProyectoTiendaOnline_U2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
