using Domain.StudentPresences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class StudentPresenceMapping : IEntityTypeConfiguration<StudentPresence>
    {
        public void Configure(EntityTypeBuilder<StudentPresence> builder)
        {
            builder.HasKey(s => new { s.ClassroomId, s.StudentId });
            
            builder.Property(s => s.ClassroomId)
                .IsRequired();
            builder.Property(s => s.StudentId)
                .IsRequired();
        }
    }
}