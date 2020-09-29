using API_Med.Dtos.Appointment;
using API_Med.Dtos.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_Med.Dtos.Service
{
    public class ServiceReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /*
        [JsonIgnore]
        [IgnoreDataMember]
        public List<AppointmentReadDto> Appointments { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<EventReadDto> Events { get; set; }
        */
    }
}
