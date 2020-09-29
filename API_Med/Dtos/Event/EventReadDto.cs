using API_Med.Dtos.Appointment;
using API_Med.Dtos.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Med.Dtos.Event
{
    public class EventReadDto
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public int ServiceId { get; set; }
        
        public int? AppointmentId { get; set; }


        [ForeignKey("ServiceId")]
        public ServiceReadDto Service { get; set; }

        [ForeignKey("AppointmentId")]
        public AppointmentReadDto Appointment { get; set; }
    }
}
