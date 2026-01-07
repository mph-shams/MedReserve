namespace MedReserve.Application.DTOs.Schedules
{
    public class CreateScheduleRequest
    {

        public int DayOfWeek { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    
    }

}
