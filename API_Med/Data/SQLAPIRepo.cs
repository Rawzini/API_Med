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

        //Обновляет данные в таблице расписания
        public void BindAppointmentToEvent(Event ev)
        {
            //
        }

        //Возвращает представление состоящее из даты, в которую пациент может пройти все назначенные процедуры
        //и список всех доступных в этот день процедур
        public ClosestDateView GetClosestSuitableDate(int id)
        {
            var appointmentsList = _context.Appointment.Where(i => i.PatientId == id).ToArray();
            var eventList = _context.Event.ToArray();
            var groups = eventList.Where(z => z.AppointmentId == null).GroupBy(x => x.DateTime.Date);
            
            for (var j = 0; j < groups.Count(); j++)
            {
                for (var i = 0; i < appointmentsList.Length; i++)
                {
                   if (!groups.ElementAt(j).Select(x => x.ServiceId).Contains(appointmentsList[i].ServiceId)) goto end_for_with_index_j;
                }

                var freeDate = groups.ElementAt(j).Key;
                var freeEvents = _context.Event
                    .Include(a => a.Service)
                    .Where(d => d.DateTime.Date == groups.ElementAt(j).Key.Date && d.AppointmentId == null)
                    .ToList();

                return new ClosestDateView { DateTime = freeDate, Events = freeEvents };

            end_for_with_index_j: continue;
            }

            return null;
        }

        //Возвращает список записей из таблицы назначения, привязанную к ней процедуру и пациента (по id пациента)
        public IEnumerable<Appointment> GetUnattachedAppointmentsById(int id)
        {
            var ServiseIds = _context.Event.Select(i => i.AppointmentId).ToArray();
            return _context.Appointment
                .Include(a => a.Patient)
                .Include(a => a.Service)
                .Where(a => !ServiseIds.Contains(a.Id) && a.PatientId == id)
                .ToList();
        }

        //Возвращает список записей из таблицы назначения, привязанную к ней процедуру и пациента (по имени пациента)
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

        //Возвращает запись из таблицы расписания, привязанную к ней процедуру и назначение (по id)
        public Event GetEventById(int eventId)
        {
            return _context.Event
                .Include(a => a.Service)
                .Include(a => a.Appointment)
                .FirstOrDefault(e => e.Id == eventId);
        }

        //Возвращает запись из таблицы назначения, привязанную к ней процедуру и пациента (по id)
        public Appointment GetAppointmentById(int appointmentId)
        {
            return _context.Appointment
                .Include(a => a.Service)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.Id == appointmentId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
