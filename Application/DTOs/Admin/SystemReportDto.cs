namespace MedReserve.Application.DTOs.Admin
{
    public class SystemReportDto
    {
        public int TotalAppointments { get; set; }
        public int DoneAppointments { get; set; }
        public int TotalDoctors { get; set; }
        public decimal TotalRevenue { get; set; }
    }

}
