using Application.Features.MedicalFiles.Queries.GetMedicalFilesByAppointment;

namespace MedReserve.Application.DTOs.Appointments
{
    public record AppointmentDetailDto(
        int Id,
        int DoctorId,
        string DoctorName,
        DateTime AppointmentDate,
        string Status,
        string Description,
        List<MedicalFileDto> AttachedFiles
    );
}
