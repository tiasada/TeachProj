using Domain.ClassDays;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class ClassDayMapping : IEntityTypeConfiguration<ClassDay>
    {
        public void Configure(EntityTypeBuilder<ClassDay> builder)
        {
            builder.Property(s => s.Id)
                .IsRequired();
            builder.Property(s => s.Notes)
                .HasMaxLength(250);
            builder.Property(s => s.Date)
                .IsRequired();
            builder.Property(s => s.ClassroomId)
                .IsRequired();
        }
    }
}