using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MedicalFileConfiguration : IEntityTypeConfiguration<MedicalFile>
{
    public void Configure(EntityTypeBuilder<MedicalFile> builder)
    {
        builder.HasKey(m => m.Id); 
        builder.Property(m => m.FileName).HasMaxLength(255).IsRequired(); 
        builder.Property(m => m.FileContent).IsRequired(); 

  
        builder.HasOne<Appointment>()
               .WithMany(a => a.MedicalFiles)
               .HasForeignKey(m => m.AppointmentId); 
    }
}