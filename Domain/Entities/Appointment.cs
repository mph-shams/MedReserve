using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Appointment : BaseEntity
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; } = null!;
    public ICollection<MedicalFile> MedicalFiles { get; set; } = new List<MedicalFile>();

}