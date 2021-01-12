using Domain.Classrooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra
{
    public class ClassroomStudentMapping : IEntityTypeConfiguration<ClassroomStudent>
    {
        public void Configure(EntityTypeBuilder<ClassroomStudent> builder)
        {
            builder.HasKey(s => new { s.ClassroomId, s.StudentId });
            
            builder.Property(s => s.ClassroomId)
                .IsRequired();
            builder.Property(s => s.StudentId)
                .IsRequired();
        }
    }
}