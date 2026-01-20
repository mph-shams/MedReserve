using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(s => s.Id); 
        builder.Property(s => s.StartTime).IsRequired(); 
        builder.Property(s => s.EndTime).IsRequired(); 


        builder.HasOne<Doctor>()
               .WithMany(d => d.Schedules)
               .HasForeignKey(s => s.DoctorId);
    }
}