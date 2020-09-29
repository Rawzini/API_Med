using API_Med.Dtos.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_Med.Dtos.Patient
{
    public class PatientReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /*
        [JsonIgnore]
        [IgnoreDataMember]
        public List<AppointmentReadDto> Appointments { get; set; }
       */
    }
}
