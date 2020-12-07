using System;
using System.Collections.Generic;

namespace Domain.Classrooms
{
    public class ClassroomsService
    {
        private readonly ClassroomsRepository _classroomsRepository = new ClassroomsRepository();
        
        public CreatedClassroomDTO Create(string name)
        {
            var classroom = new Classroom(name);
            // var classroomVal = classroom.Validate();

            // if (!classroomVal.isValid)
            // {
            //     return new CreatedClassroomDTO(classroomVal.errors);
            // }
            
            _classroomsRepository.Add(classroom);
            return new CreatedClassroomDTO(classroom.Id);
        }

        public Guid? Remove(Guid id)
        {
            return _classroomsRepository.Remove(id);
        }

        public Classroom GetByID(Guid id)
        {
            return _classroomsRepository.GetByID(id);
        }

        public IEnumerable<Classroom> GetAll()
        {
            return _classroomsRepository.GetAll();
        }
    }
}