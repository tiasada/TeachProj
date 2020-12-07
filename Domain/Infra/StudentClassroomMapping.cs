using Domain.StudentClassrooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra
{
    public class StudentClassroomMapping : IEntityTypeConfiguration<StudentClassroom>
    {
        public void Configure(EntityTypeBuilder<StudentClassroom> builder)
        {
            builder.HasKey(sc => new { sc.StudentId, sc.ClassroomId });
        }
    }
}