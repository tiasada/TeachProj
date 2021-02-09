using Domain.Parents;
using Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
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

            builder.Property(s => s.Registration)
                .IsRequired();

            builder.HasIndex(s => s.Registration)
                .IsUnique();

            builder.Property(s => s.UserId)
                .IsRequired();

            builder.HasIndex(s => s.UserId)
                .IsUnique();

            builder.HasOne(a => a.Parent)
                .WithOne(b => b.Student)
                .HasForeignKey<Parent>(c => c.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}