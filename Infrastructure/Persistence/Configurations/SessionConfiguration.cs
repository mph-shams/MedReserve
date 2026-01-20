using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(s => s.Id); 
        builder.Property(s => s.RefreshToken).IsRequired();
        builder.Property(s => s.IpAddress).HasMaxLength(50); 
        builder.Property(s => s.Device).HasMaxLength(200);  
    }
}