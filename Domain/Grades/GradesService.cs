using System;
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
        
        public CreatedGradeDTO Create(string name, string description, DateTime date, Guid classroomId)
        {
            var classroom = _classroomsService.Get(x => x.Id == classroomId);

            if (classroom == null)
            {
                return new CreatedGradeDTO("Classroom not found!");
            }
            
            var grade = new Grade(name, description, date, classroom);
            _gradesRepository.Add(grade);
            return new CreatedGradeDTO(grade.Id);
        }
    }
}