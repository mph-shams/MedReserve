namespace MedReserve.Application.DTOs.MedicalFiles
{
    public class MedicalFileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public ulong Size { get; set; }
    }

}
