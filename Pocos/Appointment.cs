using System;
namespace AppointmentEntry.Pocos
{
	public class Appointment
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string Type { get; set; }
		public string WorkRelated { get; set; }

        public Appointment(string title, string description, string date, string time, string type, string workrelated )
        {
            Title = title;
            Description = description;
            Date = date;
            Time = time;
            Type = type;
            WorkRelated = workrelated;
        }

	}
}
