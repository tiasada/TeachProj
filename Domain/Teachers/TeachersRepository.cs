using System;
using System.Linq;
using Domain.Infra;

namespace Domain.Teachers
{
    public class TeachersRepository : Repository<Teacher>, ITeachersRepository
    {
        private readonly IRepository<Teacher> _repository;
        public TeachersRepository(IRepository<Teacher> repository)
        {
            _repository = repository;
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
    }
}