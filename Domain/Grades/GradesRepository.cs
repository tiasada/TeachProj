using System;
using System.Linq;
using Domain.Infra;
using Domain.Infra.Generics;

namespace Domain.Grades
{
    public class GradesRepository : Repository<Grade>, IGradesRepository
    {
        private readonly IRepository<Grade> _repository;
        public GradesRepository(IRepository<Grade> repository)
        {
            _repository = repository;
        }

        public override void Add(Grade grade)
        {
            using (var db = new TeachContext())
            {
                db.Grades.Add(grade);

                var classroomStudents = db.Classrooms.Where(c => c.Id == grade.ClassroomId).SelectMany(c => c.Students);
                foreach (var s in classroomStudents)
                {
                    db.StudentGrades.Add(new StudentGrade(grade.Id, s.Id));
                    // db.Entry(db.Students.FirstOrDefault(x => x.Id == s.Id)).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                db.Entry(db.Classrooms.FirstOrDefault(x => x.Id == grade.ClassroomId)).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}