namespace MedReserve.Application.DTOs.Doctors
{
    public record UpdateDoctorProfileRequest(
        string Specialty,
        string Bio,
        decimal ConsultationFee
    );
}
