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

        public Guid? AddClass(Guid id, Guid classId)
        {
            using (var db = new TeachContext())
            {
                var student = db.Students.FirstOrDefault(x => x.Id == id);
                if (student == null) {return null;}
                
                db.Students.Remove(student);
                student.ClassIds.Add(classId);
                db.Students.Add(student);
                db.SaveChanges();
                return id;
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