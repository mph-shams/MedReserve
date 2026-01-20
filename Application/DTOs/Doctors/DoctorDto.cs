namespace MedReserve.Application.DTOs.Doctors
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Specialty { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
    }

}
