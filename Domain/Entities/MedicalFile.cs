using Domain.Common;

namespace Domain.Entities;

public class MedicalFile : BaseEntity
{
    public int AppointmentId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public byte[] FileContent { get; set; } = [];
    public string ContentType { get; set; } = string.Empty;
    public ulong Size { get; set; }

    public ICollection<MedicalFile> MedicalFiles { get; set; } = new List<MedicalFile>();
}