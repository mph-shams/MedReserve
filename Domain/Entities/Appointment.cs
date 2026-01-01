using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Appointment : BaseEntity
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
    public ICollection<MedicalFile> MedicalFiles { get; set; } = new List<MedicalFile>();

}