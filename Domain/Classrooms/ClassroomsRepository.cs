using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Classrooms
{
    class ClassroomsRepository
    {
        public void Add(Classroom classroom)
        {
            using (var db = new TeachContext())
            {
                db.Classrooms.Add(classroom);
                db.SaveChanges();
            }
        }

        public Guid? Remove(Guid id)
        {
            using (var db = new TeachContext())
            {
                var classroom = db.Classrooms.FirstOrDefault(x => x.Id == id);
                if (classroom == null) {return null;}
                db.Classrooms.Remove(classroom);
                db.SaveChanges();
                return id;
            }
        }

        public Classroom GetByID(Guid id)
        {
            using (var db = new TeachContext())
            {
                return db.Classrooms.FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Classroom> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Classrooms.ToList();
            }
        }
    }
}