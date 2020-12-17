using System;
using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Infra;

namespace Domain.Students
{
    public class StudentsService : Service<Student>, IStudentsService
    {
        private readonly new IStudentsRepository _repository;
        
        public StudentsService(StudentsRepository studentsRepository) : base(studentsRepository)
        {}

        public CreatedStudentDTO Create(string name, string cpf, string registration)
        {
            var student = new Student(name, cpf, registration);
            var studentVal = student.Validate();

            if (!studentVal.isValid)
            {
                return new CreatedStudentDTO(studentVal.errors);
            }
            
            _repository.Add(student);
            return new CreatedStudentDTO(student.Id);
        }
        
        public string AddClass(Guid id, Guid classId)
        {
            return _repository.AddClass(id, classId);
        }
    }
}