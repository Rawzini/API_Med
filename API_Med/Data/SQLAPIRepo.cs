using API_Med.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_Med.Data
{
    public class SQLAPIRepo : IAPIRepo
    {
        private readonly APIContext _context;

        public SQLAPIRepo(APIContext context)
        {
            _context = context;
        }

        public Event GetEventById(int eventId)
        {
            return _context.Event.FirstOrDefault(e => e.Id == eventId);
        }

        public Appointment FindAppointmentById(int appointmentId)
        {
            return _context.Appointment.FirstOrDefault(a => a.Id == appointmentId);
        }

        public void BindAppointmentToEvent(Event ev)
        {
            //
        }

        public ClosestDateView GetClosestSuitableDate(int id)
        {
            var appointmentsList = _context.Appointment.Where(i => i.PatientId == id).ToArray();

            IEnumerable<Event> eventList = _context.Event.ToArray();
            var groups = eventList.Where(z => z.AppointmentId == null).GroupBy(x => x.DateTime.Date);
            
            for (var j = 0; j < groups.Count(); j++)
            {
                for (var i = 0; i < appointmentsList.Length; i++)
                {
                   if (!groups.ElementAt(j).Select(x => x.ServiceId).Contains(appointmentsList[i].ServiceId)) goto goto1;
                }
                var freeDate = groups.ElementAt(j).Key;
                var freeEvents = _context.Event
                    .Include(a => a.Service)
                    .Where(d => d.DateTime.Date == groups.ElementAt(j).Key.Date && d.AppointmentId == null).ToList();
                return new ClosestDateView { DateTime = freeDate, Events = freeEvents };
            goto1: continue;
            }

            return null;
        }

        public IEnumerable<Appointment> GetUnattachedAppointmentsById(int id)
        {
            var ServiseIds = _context.Event.Select(i => i.AppointmentId).ToArray();
            return _context.Appointment
                .Include(a => a.Patient)
                .Include(a => a.Service)
                .Where(a => !ServiseIds.Contains(a.Id) && a.PatientId == id)
                .ToList();
        }

        public IEnumerable<Appointment> GetUnattachedAppointmentsByName(string name)
        {
            var ServiseIds = _context.Event.Select(i => i.AppointmentId).ToArray();
            var EncodedName = HttpUtility.UrlDecode(name);
            return _context.Appointment
                .Include(a => a.Patient)
                .Include(a => a.Service)
                .Where(a => !ServiseIds.Contains(a.Id) && a.Patient.Name == EncodedName)
                .ToList();

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
