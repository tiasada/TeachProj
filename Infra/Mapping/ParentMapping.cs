using Domain.Parents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class ParentMapping : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
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

            builder.Property(s => s.PhoneNumber)
                .IsRequired();

            builder.HasIndex(s => s.PhoneNumber)
                .IsUnique();

            builder.Property(s => s.BirthDate)
                .IsRequired();

            builder.HasIndex(s => s.Email)
                .IsUnique();

            builder.Property(s => s.UserId)
                .IsRequired();

            builder.HasIndex(s => s.UserId)
                .IsUnique();

            builder.Property(s => s.StudentId)
                .IsRequired();

            builder.HasIndex(s => s.StudentId)
                .IsUnique();
        }
    }
}