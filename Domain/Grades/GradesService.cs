using System;
using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Infra.Generics;

namespace Domain.Grades
{
    public class GradesService : Service<Grade>, IGradesService
    {
        protected readonly IGradesRepository _gradesRepository;
        protected readonly IClassroomsService _classroomsService;

        public GradesService(IGradesRepository gradesRepository, IClassroomsService classroomsService) : base(gradesRepository)
        {
            _gradesRepository = gradesRepository;
            _classroomsService = classroomsService;
        }
        
        public CreatedGradeDTO Create(string name, string description, Guid classroomId)
        {
            var classroom = _classroomsService.Get(x => x.Id == classroomId);

            if (classroom == null)
            {
                return new CreatedGradeDTO("Classroom not found!");
            }
            
            var grade = new Grade(name, description, DateTime.Now, classroom);
            _gradesRepository.Add(grade);

            return new CreatedGradeDTO(grade.Id);
        }

        public string SetGrade(Guid id, Guid studentId, double value)
        {
            return _gradesRepository.SetGrade(id, studentId, value);
        }

        public string CloseGrade(Guid id)
        {
            return _gradesRepository.CloseGrade(id);
        }
    }
}