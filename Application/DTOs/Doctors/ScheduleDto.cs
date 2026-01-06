namespace MedReserve.Application.DTOs.Doctors;

public class ScheduleDto
{
    public int Id { get; set; }
    public int DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int SlotDuration { get; set; }
}