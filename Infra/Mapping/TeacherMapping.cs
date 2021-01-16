using Domain.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class TeacherMapping : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(s => s.Id)
                .IsRequired();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.CPF)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(s => s.CPF)
                .IsUnique();

            builder.Property(s => s.UserId)
                .IsRequired();

            builder.HasIndex(s => s.UserId)
                .IsUnique();
        }
    }
}