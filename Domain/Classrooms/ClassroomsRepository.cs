using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Classrooms
{
    public class ClassroomsRepository : Repository<Classroom>, IClassroomsRepository
    {
        public string AddStudent(Guid id, Guid classId)
        {
            using (var db = new TeachContext())
            {
                var student = db.Students.FirstOrDefault(s => s.Id == id);
                if (student == null) { return "Student not found"; }
                var classroom = db.Classrooms.FirstOrDefault(c => c.Id == classId);
                if (classroom == null) { return "Classroom not found"; }

                if (classroom.Students.Contains(student)) {return "Student already in classroom";}
                
                classroom.Students.Add(student);

                db.Classrooms.Attach(classroom);
                db.Entry(classroom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return null;
            }
        }

        public string AddTeacher(Guid id, Guid classId)
        {
            using (var db = new TeachContext())
            {
                var teacher = db.Teachers.FirstOrDefault(s => s.Id == id);
                if (teacher == null) { return "Teacher not found"; }
                var classroom = db.Classrooms.FirstOrDefault(c => c.Id == classId);
                if (classroom == null) { return "Classroom not found"; }

                if (classroom.Teachers.Contains(teacher)) {return "Teacher already in classroom";}
                
                classroom.Teachers.Add(teacher);

                db.Classrooms.Attach(classroom);
                db.Entry(classroom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return null;
            }
        }
    }
}