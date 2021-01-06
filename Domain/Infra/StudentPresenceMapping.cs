using Domain.ClassDays;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra
{
    public class StudentPresenceMapping : IEntityTypeConfiguration<StudentPresence>
    {
        public void Configure(EntityTypeBuilder<StudentPresence> builder)
        {
            builder.Property(s => s.Id)
                .IsRequired();
            builder.Property(s => s.ClassDayId)
                .IsRequired();
            builder.Property(s => s.StudentId)
                .IsRequired();
            builder.Property(s => s.Reason)
                .HasMaxLength(100);
        }
    }
}