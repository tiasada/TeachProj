using System;
using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Infra;

namespace Domain.Teachers
{
    public class TeachersService : Service<Teacher>, ITeachersService
    {
        private readonly new ITeachersRepository _repository;
        
        public TeachersService(TeachersRepository teachersRepository) : base(teachersRepository)
        {}
        
        public CreatedTeacherDTO Create(string name, string cpf)
        {
            var teacher = new Teacher(name, cpf);
            var teacherVal = teacher.Validate();

            if (!teacherVal.isValid)
            {
                return new CreatedTeacherDTO(teacherVal.errors);
            }
            
            _repository.Add(teacher);
            return new CreatedTeacherDTO(teacher.Id);
        }

        public string AddClass(Guid id, Guid classId)
        {
            return _repository.AddClass(id, classId);
        }
    }
}