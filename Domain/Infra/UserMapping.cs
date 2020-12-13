using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id)
                .IsRequired();

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Profile)
                .IsRequired();
        }
    }
}