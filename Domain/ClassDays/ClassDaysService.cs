using System;
using Domain.Classrooms;
using Domain.Common;
using Domain.Students;

namespace Domain.ClassDays
{
    public class ClassDaysService : Service<ClassDay>, IClassDaysService
    {
        protected readonly IClassDaysRepository _repository;
        protected readonly IClassroomsService _classroomsService;
        protected readonly IStudentsService _studentsService;

        public ClassDaysService(IClassDaysRepository classDaysRepository,
                                IClassroomsService classroomsService,
                                IStudentsService studentsService) : base(classDaysRepository)
        {
            _repository = classDaysRepository;
            _classroomsService = classroomsService;
            _studentsService = studentsService;
        }
        
        public CreatedEntityDTO Create(DateTime date, Guid classroomId, string notes)
        {
            var classroom = _classroomsService.Get(x => x.Id == classroomId);

            if (classroom == null)
            {
                return new CreatedEntityDTO("Classroom not found!");
            }
            
            var classday = new ClassDay(date, classroom, notes);
            _repository.Add(classday);

            return new CreatedEntityDTO(classday.Id);
        }

        public string SetPresence(Guid id, Guid studentId, bool present, string reason)
        {
            var student = _studentsService.Get(studentId);
            if (student == null) { return "Student not found"; }
            var classDay = _repository.Get(id);
            if (classDay == null) { return "Class day not found"; }
            
            var classroomId = _repository.Get(id).ClassroomId;
            var studentEligible = _classroomsService.GetStudent(classroomId, studentId);
            if (studentEligible == null) { return "Student not eligible"; }

            var studentPresence = new StudentPresence(classDay, student, present, reason);
            _repository.SetPresence(studentPresence);

            return null;
        }
    }
}