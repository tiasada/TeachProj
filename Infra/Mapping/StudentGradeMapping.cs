using Domain.Grades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class StudentGradeMapping : IEntityTypeConfiguration<StudentGrade>
    {
        public void Configure(EntityTypeBuilder<StudentGrade> builder)
        {
            builder.HasKey(s => new { s.BaseGradeId, s.StudentId });
            
            builder.Property(s => s.BaseGradeId)
                .IsRequired();
            builder.Property(s => s.StudentId)
                .IsRequired();
        }
    }
}