using API_Med.Dtos.Event;
using API_Med.Dtos.Patient;
using API_Med.Dtos.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_Med.Dtos.Appointment
{
    public class AppointmentReadDto
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int ServiceId { get; set; }


        [ForeignKey("ServiceId")]
        public ServiceReadDto Service { get; set; }

        [ForeignKey("PatientId")]
        public PatientReadDto Patient { get; set; }

        /*
        [JsonIgnore]
        [IgnoreDataMember]
        public List<EventReadDto> Events { get; set; }
        */
    }
}
