namespace MedReserve.Application.DTOs.Doctors
{
    public class DoctorDetailsDto
    {
        public int Id { get; set; }
        public string Specialty { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public decimal Fee { get; set; }
    }

}
