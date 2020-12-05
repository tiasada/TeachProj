using System;
using System.Collections.Generic;

namespace Domain.Classrooms
{
    public class ClassroomsService
    {
        private readonly ClassroomsRepository _classroomsRepository = new ClassroomsRepository();
        
        public Guid Create(string name)
        {
            var classroom = new Classroom(name);

            _classroomsRepository.Add(classroom);
            return classroom.Id;
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