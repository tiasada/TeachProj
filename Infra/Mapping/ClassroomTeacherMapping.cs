using Domain.Classrooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class ClassroomTeacherMapping : IEntityTypeConfiguration<ClassroomTeacher>
    {
        public void Configure(EntityTypeBuilder<ClassroomTeacher> builder)
        {
            builder.HasKey(s => new { s.ClassroomId, s.TeacherId });
            
            builder.Property(s => s.ClassroomId)
                .IsRequired();
            builder.Property(s => s.TeacherId)
                .IsRequired();
        }
    }
}