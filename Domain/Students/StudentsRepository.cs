using System;
using System.Linq;
using Domain.Infra;
using Domain.Infra.Generics;

namespace Domain.Students
{
    public class StudentsRepository : Repository<Student>, IStudentsRepository
    {
        public string AddClass(Guid id, Guid classId)
        {
            using (var db = new TeachContext())
            {
                var student = db.Students.FirstOrDefault(s => s.Id == id);
                if (student == null) { return "Student not found"; }
                var classroom = db.Classrooms.FirstOrDefault(c => c.Id == classId);
                if (classroom == null) { return "Classroom not found"; }

                if (student.Classrooms.Contains(classroom)) {return "Student already in classroom";}
                
                student.Classrooms.Add(classroom);

                db.Students.Attach(student);
                db.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return null;
            }
        }
    }
}