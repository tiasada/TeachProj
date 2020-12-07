using System;
using System.Collections.Generic;
using Domain.Classrooms;

namespace Domain.Teachers
{
    public class TeachersService
    {
        private readonly TeachersRepository _teachersRepository = new TeachersRepository();
        private readonly ClassroomsService _classroomsService = new ClassroomsService();
        
        public CreatedTeacherDTO Create(string name, string cpf)
        {
            var teacher = new Teacher(name, cpf);
            var teacherVal = teacher.Validate();

            if (!teacherVal.isValid)
            {
                return new CreatedTeacherDTO(teacherVal.errors);
            }
            
            _teachersRepository.Add(teacher);
            return new CreatedTeacherDTO(teacher.Id);
        }

        public Guid? Remove(Guid id)
        {
            return _teachersRepository.Remove(id);
        }

        public string AddClass(Guid id, Guid classId)
        {
            return _teachersRepository.AddClass(id, classId);
        }

        public Teacher GetByID(Guid id)
        {
            return _teachersRepository.GetByID(id);
        }

        public IEnumerable<Teacher> GetAll()
        {
            return _teachersRepository.GetAll();
        }
    }
}