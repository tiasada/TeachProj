using Domain.Common;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var crypt = new Crypt();
            var cryptPassword = crypt.CreateMD5("admin123");
            builder.HasData(new User(0, "Admin", cryptPassword));
            
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