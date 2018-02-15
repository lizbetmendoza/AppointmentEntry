using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Services = AppointmentEntry.Services;
using Pocos = AppointmentEntry.Pocos;
using Newtonsoft.Json;

namespace AppointmentEntry.Controllers
{
    public class AppointmentController : Controller
    {
        public ActionResult Index()
        {
            return View ("~/Views/Index.cshtml");
        }


        [HttpGet]
        public string Appointments() {
            // not been used, I try to do an XML to JSON convertion but I fail ;(
            return Services.Appointment.GetAll();

        }

        [HttpGet]
        public string GetAppointment(string title) {
            // call from Edit acction, so we get the appointment we need to work on
            return Services.Appointment.GetBy(title);
        }

        [HttpPost]
        public bool Insert(string title, string description, string date, string time, string type, string workRelated)
        {
            // call when we click save when adding a new appointment
            return Services.Appointment.Insert(
                new Pocos.Appointment(
                    title,
                    description,
                    date,
                    time,
                    type,
                    workRelated
                ));
        }

        [HttpPost]
        public bool Update(string title, string description, string date, string time, string type, string workRelated)
        {   
            // call from save whenever we are editing an appointment
            return Services.Appointment.Update(
                new Pocos.Appointment(
                    title, 
                    description, 
                    date, 
                    time, 
                    type, 
                    workRelated
                ));
        }

        [HttpPost]
        public bool Delete(string title)
        {
            // call when we click delete in the grid
            return Services.Appointment.Delete(title); ;
        }

    }
}
