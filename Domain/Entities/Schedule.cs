using Domain.Common;

namespace Domain.Entities;

public class Schedule : BaseEntity 
{
    public int DoctorId { get; set; }
    public int DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int SlotDuration { get; set; }
}