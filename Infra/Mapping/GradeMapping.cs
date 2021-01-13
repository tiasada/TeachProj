using Domain.Grades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class GradeMapping : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.Property(s => s.Id)
                .IsRequired();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Description)
                .HasMaxLength(250);
            builder.Property(s => s.Subject)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Date)
                .IsRequired();
            builder.Property(s => s.ClassroomId)
                .IsRequired();
        }
    }
}