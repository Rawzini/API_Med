using API_Med.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Med.Data
{
    public interface IAPIRepo
    {
        bool SaveChanges();

        IEnumerable<Appointment> GetUnattachedAppointmentsByName(string name);
        IEnumerable<Appointment> GetUnattachedAppointmentsById(int id);
        ClosestDateView GetClosestSuitableDate(int id);
        void BindAppointmentToEvent(Event ev);
        Event GetEventById(int eventId);
        Appointment GetAppointmentById(int appointmentId);
    }
}
