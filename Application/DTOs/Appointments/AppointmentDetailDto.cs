using MedReserve.Application.DTOs.MedicalFiles;

namespace MedReserve.Application.DTOs.Appointments
{
    public class AppointmentDetailDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
