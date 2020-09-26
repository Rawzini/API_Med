using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_Med.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<Appointment> Appointments { get; set; }
        [JsonIgnore]
        [IgnoreDataMember] 
        public List<Event> Events { get; set; }

    }
}
