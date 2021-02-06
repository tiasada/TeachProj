using System;
using Domain.Classrooms;
using Domain.Common;
using Domain.Students;

namespace Domain.Grades
{
    public class GradesService : Service<Grade>, IGradesService
    {
        protected readonly IGradesRepository _repository;
        protected readonly IClassroomsService _classroomsService;
        protected readonly IStudentsService _studentsService;
        protected readonly IRepository<StudentGrade> _studentGradesRepository;
        protected readonly IRepository<ClassroomStudent> _classStudentsRepository;

        public GradesService(IGradesRepository gradesRepository,
                            IClassroomsService classroomsService,
                            IStudentsService studentsService,
                            IRepository<StudentGrade> studentGradesRepository,
                            IRepository<ClassroomStudent> classStudentsRepository
                            ) : base(gradesRepository)
        {
            _repository = gradesRepository;
            _classroomsService = classroomsService;
            _studentsService = studentsService;
            _studentGradesRepository = studentGradesRepository;
            _classStudentsRepository = classStudentsRepository;
        }
        
        public CreatedEntityDTO Create(string name, string description, string subject, DateTime date, Guid classroomId)
        {
            var classroom = _classroomsService.Get(x => x.Id == classroomId);

            if (classroom == null)
            {
                return new CreatedEntityDTO("Classroom not found!");
            }
            
            var grade = new Grade(name, description, subject, date.Date, classroom);
            _repository.Add(grade);

            return new CreatedEntityDTO(grade.Id);
        }

        public string SetGrade(Guid id, Guid studentId, double value)
        {
            var student = _studentsService.Get(studentId);
            if (student == null) { return "Student not found"; }
            
            var grade = _repository.Get(x => x.Id == id);
            if (grade == null) { return "Grade not found"; }
            if (grade.IsClosed) { return "Grade closed"; }
            
            var studentEligible = _classStudentsRepository.Get(x => x.ClassroomId == grade.ClassroomId && x.StudentId == studentId);
            if (studentEligible == null) { return "Student not eligible"; }

            var relation = new StudentGrade(grade, student, value);
            _studentGradesRepository.Add(relation);
            
            return null;
        }

        public string CloseGrade(Guid id)
        {
            var grade = _repository.Get(x => x.Id == id);
            if (grade == null) { return "Grade not found"; }
            if (grade.IsClosed) { return "Grade already closed"; }
            
            grade.IsClosed = true;
            grade.DateClosed = DateTime.Now.Date;

            _repository.Modify(grade);
            
            return null;
        }
    }
}