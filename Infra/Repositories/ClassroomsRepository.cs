using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Classrooms;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ClassroomsRepository : DatabaseRepository<Classroom>, IClassroomsRepository
    {
        public override Classroom Get(Func<Classroom, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.Classrooms
                    .Include(x => x.ClassDays)
                    .Include(x => x.Grades)
                    .Include(x => x.Students).ThenInclude(s => s.Student)
                    .Include(x => x.Teachers).ThenInclude(t => t.Teacher)
                    .FirstOrDefault(predicate);
            }
        }
        
        public override IEnumerable<Classroom> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Classrooms
                    .Include(x => x.ClassDays)
                    .Include(x => x.Grades)
                    .Include(x => x.Students).ThenInclude(s => s.Student)
                    .Include(x => x.Teachers).ThenInclude(t => t.Teacher)
                    .ToList();
            }
        }

        public override IEnumerable<Classroom> GetAll(Func<Classroom, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}