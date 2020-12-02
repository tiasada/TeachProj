using System;
using System.Collections.Generic;

namespace Domain.Students
{
    public class StudentsService
    {
        private readonly StudentsRepository _studentsRepository = new StudentsRepository();
        
        public CreatedStudentDTO Create(string name, string cpf)
        {
            var student = new Student(name, cpf);
            var studentVal = student.Validate();

            if (!studentVal.isValid)
            {
                return new CreatedStudentDTO(studentVal.errors);
            }
            
            _studentsRepository.Add(student);
            return new CreatedStudentDTO(student.Id);
        }

        public CreatedStudentDTO Update(Guid id, string name, string cpf)
        {
            var student = new Student(name, cpf);
            var studentValidation = student.Validate();

            if (!studentValidation.isValid)
            {
                return new CreatedStudentDTO(studentValidation.errors);
            }
            
            _studentsRepository.Remove(id);
            _studentsRepository.Add(student);
            return new CreatedStudentDTO(student.Id);
        }

        public Guid? Remove(Guid id)
        {
            return _studentsRepository.Remove(id);
        }

        public Student GetByID(Guid id)
        {
            return _studentsRepository.GetByID(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentsRepository.GetAll();
        }
    }
}