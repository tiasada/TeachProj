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

        public void SetPresence(StudentPresence studentPresence)
        {
            _presenceRepository.Add(studentPresence);
        }
        
        public override ClassDay Get(Func<ClassDay, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.ClassDays
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