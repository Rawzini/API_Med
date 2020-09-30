using API_Med.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
