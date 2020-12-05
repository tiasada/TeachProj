using System;
using System.Collections.Generic;
using Domain.Classrooms;

namespace Domain.Students
{
    public class StudentsService
    {
        private readonly StudentsRepository _studentsRepository = new StudentsRepository();
        private readonly ClassroomsService _classroomsService = new ClassroomsService();
        
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

        public Guid? Remove(Guid id)
        {
            return _studentsRepository.Remove(id);
        }

        public Guid? AddClass(Guid id, Guid classId)
        {
            if (_classroomsService.GetByID(classId) == null)
            {
                return null;
            }

            return _studentsRepository.AddClass(id, classId);
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