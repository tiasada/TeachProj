using System;
using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Infra;

namespace Domain.Students
{
    public class StudentsService : Service<Student>, IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        
        public StudentsService(IStudentsRepository studentsRepository) : base(studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public CreatedStudentDTO Create(string name, string cpf, string registration)
        {
            if (_studentsRepository.Get(x => x.CPF == cpf || x.Registration == registration) != null)
            {
                return new CreatedStudentDTO(new List<string>{"Student already exists"});
            }
            
            var student = new Student(name, cpf, registration);
            var studentVal = student.Validate();

            if (!studentVal.isValid)
            {
                return new CreatedStudentDTO(studentVal.errors);
            }
            
            _studentsRepository.Add(student);
            return new CreatedStudentDTO(student.Id);
        }
        
        public string AddClass(Guid id, Guid classId)
        {
            return _studentsRepository.AddClass(id, classId);
        }
    }
}