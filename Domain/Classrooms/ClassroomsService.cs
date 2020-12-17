using System;
using System.Collections.Generic;
using Domain.Infra;

namespace Domain.Classrooms
{
    public class ClassroomsService : Service<Classroom>, IClassroomsService
    {
        private readonly new IClassroomsRepository _repository;
        
        public ClassroomsService(ClassroomsRepository classroomsRepository) : base(classroomsRepository)
        {}
        
        public CreatedClassroomDTO Create(string name)
        {
            var classroom = new Classroom(name);
            _repository.Add(classroom);
            return new CreatedClassroomDTO(classroom.Id);
        }

        public string AddStudent(Guid id, Guid classId)
        {
            return _repository.AddStudent(id, classId);
        }

        public string AddTeacher(Guid id, Guid classId)
        {
            return _repository.AddTeacher(id, classId);
        }
    }
}