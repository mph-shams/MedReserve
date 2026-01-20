using Domain.Common;

namespace Domain.Entities;

public class Doctor : BaseEntity
{
    public int UserId { get; set; }
    public string Specialty { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public decimal ConsultationFee { get; set; }

    public User User { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}