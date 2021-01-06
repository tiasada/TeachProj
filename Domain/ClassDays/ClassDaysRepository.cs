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

                var classroomStudents = db.Classrooms.Where(c => c.Id == classDay.ClassroomId).SelectMany(c => c.Students);
                foreach (var s in classroomStudents)
                {
                    db.StudentPresences.Add(new StudentPresence(classDay.Id, s.Id));
                }

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
                
                var studentPresence = db.StudentPresences.FirstOrDefault(x => x.StudentId == studentId && x.ClassDayId == id);
                if (studentPresence == null) { return "Student not eligible"; }

                studentPresence.Present = present;
                studentPresence.Reason = reason;
                db.StudentPresences.Attach(studentPresence);
                db.Entry(studentPresence).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return null;
            }
        }
    }
}