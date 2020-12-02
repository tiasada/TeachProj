using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Students
{
    class StudentsRepository
    {
        public void Add(Student player)
        {
            using (var db = new TeachContext())
            {
                db.Students.Add(player);
                db.SaveChanges();
            }
        }

        public Guid? Remove(Guid id)
        {
            using (var db = new TeachContext())
            {
                var player = db.Students.FirstOrDefault(x => x.Id == id);
                if (player == null) {return null;}
                db.Students.Remove(player);
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
                return db.Students;
            }
        }
    }
}