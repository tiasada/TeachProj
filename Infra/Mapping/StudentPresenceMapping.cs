using Domain.ClassDays;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class StudentPresenceMapping : IEntityTypeConfiguration<StudentPresence>
    {
        public void Configure(EntityTypeBuilder<StudentPresence> builder)
        {
            builder.HasKey(s => new { s.ClassDayId, s.StudentId });

            builder.Property(s => s.ClassDayId)
                .IsRequired();
            builder.Property(s => s.StudentId)
                .IsRequired();
            builder.Property(s => s.Present)
                .IsRequired();
            builder.Property(s => s.Reason)
                .HasMaxLength(100);
        }
    }
}