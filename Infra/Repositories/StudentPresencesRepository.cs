using System;
using System.Collections.Generic;
using System.Linq;
using Domain.ClassDays;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class StudentPresencesRepository : DatabaseRepository<StudentPresence>, IStudentPresencesRepository
    {
        public override void Add(StudentPresence presence)
        {
            using (var db = new TeachContext())
            {
                db.StudentPresences.Add(presence);
                db.SaveChanges();
            }
        }
    }
}