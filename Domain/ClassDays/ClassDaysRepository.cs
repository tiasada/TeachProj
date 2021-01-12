using System;
using System.Linq;
using Domain.Infra;
using Domain.Infra.Generics;

namespace Domain.ClassDays
{
    public class ClassDaysRepository : Repository<ClassDay>, IClassDaysRepository
    {
        private readonly IRepository<ClassDay> _repository;
        public ClassDaysRepository(IRepository<ClassDay> repository)
        {
            _repository = repository;
        }

        public override void Add(ClassDay classDay)
        {
            using (var db = new TeachContext())
            {
                db.ClassDays.Add(classDay);

                db.Entry(db.Classrooms.FirstOrDefault(x => x.Id == classDay.ClassroomId)).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public string SetPresence(Guid id, Guid studentId, bool present, string reason)
        {
            using (var db = new TeachContext())
            {
                var student = db.Students.FirstOrDefault(s => s.Id == studentId);
                if (student == null) { return "Student not found"; }
                var classDay = db.ClassDays.FirstOrDefault(g => g.Id == id);
                if (classDay == null) { return "Class day not found"; }
                
                var studentEligible = db.ClassroomStudents.FirstOrDefault(x => x.ClassroomId == classDay.ClassroomId && x.StudentId == studentId);
                if (studentEligible == null) { return "Student not eligible"; }

                var studentPresence = new StudentPresence(id, studentId, present, reason);
                db.StudentPresences.Add(studentPresence);
                db.SaveChanges();
                return null;
            }
        }
    }
}