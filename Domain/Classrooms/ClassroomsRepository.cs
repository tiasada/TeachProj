using System;
using System.Linq;
using Domain.Infra;
using Domain.Infra.Generics;

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

                if (db.ClassroomStudents.FirstOrDefault(x => x.ClassroomId == classId && x.StudentId == id) != null) {return "Student already in classroom";}
                
                db.ClassroomStudents.Add(new ClassroomStudent(classId, id));

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

                if (db.ClassroomTeachers.FirstOrDefault(x => x.ClassroomId == classId && x.TeacherId == id) != null) {return "Teacher already in classroom";}
                
                db.ClassroomTeachers.Add(new ClassroomTeacher(classId, id));

                db.Classrooms.Attach(classroom);
                db.Entry(classroom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return null;
            }
        }

        public string AddSubjects(Guid id, string subjects)
        {
            using (var db = new TeachContext())
            {
                var classroom = db.Classrooms.FirstOrDefault(c => c.Id == id);
                if (classroom == null) { return "Classroom not found"; }

                classroom.Subjects += subjects;

                db.Classrooms.Attach(classroom);
                db.Entry(classroom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return null;
            }
        }
    }
}