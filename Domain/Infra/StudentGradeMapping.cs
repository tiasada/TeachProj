using Domain.Grades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infra
{
    public class StudentGradeMapping : IEntityTypeConfiguration<StudentGrade>
    {
        public void Configure(EntityTypeBuilder<StudentGrade> builder)
        {
            builder.Property(s => s.Id)
                .IsRequired();
            builder.Property(s => s.BaseGradeId)
                .IsRequired();
            builder.Property(s => s.StudentId)
                .IsRequired();
        }
    }
}