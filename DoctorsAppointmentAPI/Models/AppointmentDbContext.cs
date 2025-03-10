using Microsoft.EntityFrameworkCore;

namespace DoctorsAppointmentAPI.Models
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options){ }

        public DbSet <Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
