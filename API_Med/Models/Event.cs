﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Med.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public int? AppointmentId { get; set; }


        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
    }
}
