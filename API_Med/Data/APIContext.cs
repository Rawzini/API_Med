using API_Med.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Med.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> opt) : base(opt)
        {
            
        }

        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Service> Service { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(p => p.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne<Service>()
                .WithMany()
                .HasForeignKey(p => p.ServiceId);

            modelBuilder.Entity<Event>()
                .HasOne<Appointment>()
                .WithMany()
                .HasForeignKey(p => p.AppointmentId);

            modelBuilder.Entity<Event>()
                .HasOne<Service>()
                .WithMany()
                .HasForeignKey(p => p.ServiceId); 
        }
        */
    }
}
