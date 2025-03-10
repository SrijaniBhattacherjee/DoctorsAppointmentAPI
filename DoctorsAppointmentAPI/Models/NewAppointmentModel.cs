using System.ComponentModel.DataAnnotations;

namespace DoctorsAppointmentAPI.Models
{
    public class NewAppointmentModel
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime appointmentDate { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
