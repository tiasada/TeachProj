using System;
using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Infra.Generics;

namespace Domain.ClassDays
{
    public class ClassDaysService : Service<ClassDay>, IClassDaysService
    {
        protected readonly IClassDaysRepository _classDaysRepository;
        protected readonly IClassroomsService _classroomsService;

        public ClassDaysService(IClassDaysRepository classDaysRepository, IClassroomsService classroomsService) : base(classDaysRepository)
        {
            _classDaysRepository = classDaysRepository;
            _classroomsService = classroomsService;
        }
        
        public CreatedClassDayDTO Create(DateTime date, Guid classroomId, string notes)
        {
            var classroom = _classroomsService.Get(x => x.Id == classroomId);

            if (classroom == null)
            {
                return new CreatedClassDayDTO("Classroom not found!");
            }
            
            var classday = new ClassDay(date, classroom, notes);
            _classDaysRepository.Add(classday);

            return new CreatedClassDayDTO(classday.Id);
        }

        public string SetPresence(Guid id, Guid studentId, bool present, string reason)
        {
            return _classDaysRepository.SetPresence(id, studentId, present, reason);
        }
    }
}