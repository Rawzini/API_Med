﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_Med.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int ServiceId { get; set; }


        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        public List<Event> Events { get; set; }

    }
}
