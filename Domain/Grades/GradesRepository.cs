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
                db.SaveChanges();
            }
        }

        public string SetGrade(Guid id, Guid studentId, double value)
        {
            using (var db = new TeachContext())
            {
                var student = db.Students.FirstOrDefault(s => s.Id == studentId);
                if (student == null) { return "Student not found"; }
                var grade = db.Grades.FirstOrDefault(g => g.Id == id);
                if (grade == null) { return "Grade not found"; }
                if (grade.IsClosed) { return "Grade closed"; }
                
                var studentGrade = db.StudentGrades.FirstOrDefault(x => x.StudentId == studentId && x.BaseGradeId == id);
                if (studentGrade == null) { return "Student not eligible"; }

                studentGrade.Grade = value;
                db.StudentGrades.Attach(studentGrade);
                db.Entry(studentGrade).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return null;
            }
        }
        
        public string CloseGrade(Guid id)
        {
            using (var db = new TeachContext())
            {
                var grade = db.Grades.FirstOrDefault(g => g.Id == id);
                if (grade == null) { return "Grade not found"; }
                if (grade.IsClosed) { return "Grade already closed"; }

                grade.IsClosed = true;
                grade.DateClosed = DateTime.Now.Date;
                db.Grades.Attach(grade);
                db.Entry(grade).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return null;
            }
        }
    }
}