using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_Med.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public List<Event> Events { get; set; }

    }
}
