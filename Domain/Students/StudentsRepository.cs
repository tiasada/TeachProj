using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Students
{
    class StudentsRepository
    {
        public void Add(Student student)
        {
            using (var db = new TeachContext())
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
        }

        public Guid? Remove(Guid id)
        {
            using (var db = new TeachContext())
            {
                var student = db.Students.FirstOrDefault(x => x.Id == id);
                if (student == null) {return null;}
                db.Students.Remove(student);
                db.SaveChanges();
                return id;
            }
        }

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

        public Student GetByID(Guid id)
        {
            using (var db = new TeachContext())
            {
                return db.Students.FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Student> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Students.ToList();
            }
        }
    }
}