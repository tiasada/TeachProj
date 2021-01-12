using System.Collections.Generic;
using Domain.Infra.Generics;

namespace Domain.Teachers
{
    public class TeachersService : Service<Teacher>, ITeachersService
    {
        private readonly ITeachersRepository _teachersRepository;

        public TeachersService(ITeachersRepository teachersRepository) : base(teachersRepository)
        {
            _teachersRepository = teachersRepository;
        }
        
        public CreatedTeacherDTO Create(string name, string cpf)
        {
            if (_teachersRepository.Get(x => x.CPF == cpf) != null)
            {
                return new CreatedTeacherDTO(new List<string>{"Teacher already exists"});
            }
            
            var teacher = new Teacher(name, cpf);
            var teacherVal = teacher.Validate();

            if (!teacherVal.isValid)
            {
                return new CreatedTeacherDTO(teacherVal.errors);
            }
            
            _teachersRepository.Add(teacher);
            return new CreatedTeacherDTO(teacher.Id);
        }
    }
}