using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Teachers
{
    class TeachersRepository
    {
        public void Add(Teacher teacher)
        {
            using (var db = new TeachContext())
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
            }
        }

        public Guid? Remove(Guid id)
        {
            using (var db = new TeachContext())
            {
                var teacher = db.Teachers.FirstOrDefault(x => x.Id == id);
                if (teacher == null) {return null;}
                db.Teachers.Remove(teacher);
                db.SaveChanges();
                return id;
            }
        }

        public string AddClass(Guid id, Guid classId)
        {
            using (var db = new TeachContext())
            {
                var teacher = db.Teachers.FirstOrDefault(s => s.Id == id);
                if (teacher == null) { return "Teacher not found"; }
                var classroom = db.Classrooms.FirstOrDefault(c => c.Id == classId);
                if (classroom == null) { return "Classroom not found"; }

                if (teacher.Classrooms.Contains(classroom)) {return "Teacher already in classroom";}
                
                teacher.Classrooms.Add(classroom);

                db.Teachers.Attach(teacher);
                db.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return null;
            }
        }

        public Teacher GetByID(Guid id)
        {
            using (var db = new TeachContext())
            {
                return db.Teachers.FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Teacher> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Teachers.ToList();
            }
        }
    }
}