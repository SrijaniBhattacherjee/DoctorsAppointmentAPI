using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoctorsAppointmentAPI.Models
{
    [Table("Appointments")]
    public class Appointment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public DateTime appointmentDate { get; set; }
        [Required]
        public bool isDone { get; set; }
        [MaxLength(100)]
        public double Fees { get; set; } 

    }
}
