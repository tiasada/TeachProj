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

        public string AddStudent(Guid id, Guid classId)
        {
            return _classroomsRepository.AddStudent(id, classId);
        }

        public string AddTeacher(Guid id, Guid classId)
        {
            return _classroomsRepository.AddTeacher(id, classId);
        }

        public Classroom GetByID(Guid id)
        {
            return _classroomsRepository.Get(x => x.Id == id);
        }

        public IEnumerable<Classroom> GetAll()
        {
            return _classroomsRepository.GetAll();
        }
    }
}