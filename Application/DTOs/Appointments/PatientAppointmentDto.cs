namespace MedReserve.Application.DTOs.Appointments
{
    public class PatientAppointmentDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
