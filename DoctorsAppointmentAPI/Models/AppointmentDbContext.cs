using Microsoft.EntityFrameworkCore;

namespace DoctorsAppointmentAPI.Models
{
    public class AppointmentDbContext : DbContext
    {
        //Table model configurations injected for app
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options){ }

        public DbSet <Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
