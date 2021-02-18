using System;
using System.Collections.Generic;
using System.Linq;
using Domain.ClassDays;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ClassDaysRepository : DatabaseRepository<ClassDay>, IClassDaysRepository
    {
        private readonly IRepository<StudentPresence> _presenceRepository;
        public ClassDaysRepository(IRepository<StudentPresence> presenceRepository)
        {
            _presenceRepository = presenceRepository;
        }

        public override void Add(ClassDay classDay)
        {
            using (var db = new TeachContext())
            {
                db.Entry(classDay.Classroom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.ClassDays.Add(classDay);
                db.SaveChanges();
            }
        }
        
        public void SetPresence(StudentPresence studentPresence)
        {
            using (var db = new TeachContext())
            {
                // db.Entry(studentPresence.ClassDay.Classroom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _presenceRepository.Add(studentPresence);
                db.SaveChanges();
            }
        }
        
        public override ClassDay Get(Func<ClassDay, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.ClassDays
                    .Include(x => x.Classroom)
                    .Include(x => x.StudentPresences).ThenInclude(s => s.Student)
                    .FirstOrDefault(predicate);
            }
        }
        
        public override IEnumerable<ClassDay> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.ClassDays
                    .Include(x => x.StudentPresences).ThenInclude(s => s.Student)
                    .ToList();
            }
        }

        public override IEnumerable<ClassDay> GetAll(Func<ClassDay, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}