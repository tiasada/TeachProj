using System;
using Domain.Infra.Generics;

namespace Domain.Classrooms
{
    public class ClassroomsService : Service<Classroom>, IClassroomsService
    {
        protected readonly IClassroomsRepository _classroomsRepository;

        public ClassroomsService(IClassroomsRepository classroomsRepository) : base(classroomsRepository)
        {
            _classroomsRepository = classroomsRepository;
        }
        
        public CreatedClassroomDTO Create(string name)
        {
            var classroom = new Classroom(name);
            _classroomsRepository.Add(classroom);
            return new CreatedClassroomDTO(classroom.Id);
        }

        public string AddStudent(Guid id, Guid classId)
        {
            return _classroomsRepository.AddStudent(id, classId);
        }

        public string AddTeacher(Guid id, Guid classId)
        {
            return _classroomsRepository.AddTeacher(id, classId);
        }
    }
}