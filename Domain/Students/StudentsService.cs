using System.Collections.Generic;
using Domain.Common;

namespace Domain.Students
{
    public class StudentsService : Service<Student>, IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        
        public StudentsService(IStudentsRepository studentsRepository) : base(studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public CreatedEntityDTO Create(string name, string cpf, string registration)
        {
            if (_studentsRepository.Get(x => x.CPF == cpf || x.Registration == registration) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Student already exists"});
            }
            
            var student = new Student(name, cpf, registration);
            var studentVal = student.Validate();

            if (!studentVal.isValid)
            {
                return new CreatedEntityDTO(studentVal.errors);
            }
            
            _studentsRepository.Add(student);
            return new CreatedEntityDTO(student.Id);
        }
    }
}