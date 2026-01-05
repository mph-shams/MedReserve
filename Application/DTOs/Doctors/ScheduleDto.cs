namespace MedReserve.Application.DTOs.Doctors
{
    public record ScheduleDto(
        int Id,
        DayOfWeek DayOfWeek,
        TimeSpan StartTime,
        TimeSpan EndTime,
        int SlotDuration
    );
}
