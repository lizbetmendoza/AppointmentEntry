using System.Web.Mvc;
using System.Web.Routing;

namespace AppointmentEntry
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Appointment", action = "Index" }
            );

            routes.MapRoute(
                name: "GetAll",
                url: "appointments",
                defaults: new { controller = "Appointment", action = "Appointments" }
            );

            routes.MapRoute(
                name: "GetAppointment",
                url: "get/{title}",
                defaults: new { controller = "Appointment", action = "GetAppointment" }
            );

            routes.MapRoute(
                name: "Insert",
                url: "insert/{title}/{description}/{date}/{time}/{type}/{workrelated}",
                defaults: new { controller = "Appointment", action = "Insert" }
            );

            routes.MapRoute(
                name: "Update",
                url: "update/{title}/{description}/{date}/{time}/{type}/{workrelated}",
                defaults: new { controller = "Appointment", action = "Update" }
            );

            routes.MapRoute(
                name: "Delete",
                url: "delete/{title}",
                defaults: new { controller = "Appointment", action = "Delete" }
            );

            
        }
    }
}
