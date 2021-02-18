using System;
using System.Collections.Generic;
using System.Linq;
using Domain.StudentPresences;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class StudentPresencesRepository : DatabaseRepository<StudentPresence>, IStudentPresencesRepository
    {
        public override void Add(StudentPresence presence)
        {
            using (var db = new TeachContext())
            {
                db.Entry(presence.Classroom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.Entry(presence.Student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.StudentPresences.Add(presence);
                db.SaveChanges();
            }
        }
        
        public override StudentPresence Get(Func<StudentPresence, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.StudentPresences
                    .Include(x => x.Classroom)
                    .Include(x => x.Student)
                    .FirstOrDefault(predicate);
            }
        }
        
        public override IEnumerable<StudentPresence> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.StudentPresences
                    .Include(x => x.Classroom)
                    .Include(x => x.Student)
                    .ToList();
            }
        }

        public override IEnumerable<StudentPresence> GetAll(Func<StudentPresence, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}