using System;
using System.Linq;
using Domain.Common;
using Domain.Students;
using Domain.Teachers;

namespace Domain.Classrooms
{
    public class ClassroomsService : Service<Classroom>, IClassroomsService
    {
        protected readonly IClassroomsRepository _repository;
        protected readonly IRelationalRepository<ClassroomStudent> _classStudentsRepository;
        protected readonly IRelationalRepository<ClassroomTeacher> _classTeachersRepository;
        protected readonly IStudentsService _studentsService;
        protected readonly ITeachersService _teachersService;

        public ClassroomsService(IClassroomsRepository classroomsRepository,
                                IStudentsService studentsService,
                                ITeachersService teachersService,
                                IRelationalRepository<ClassroomStudent> classStudentsRepository,
                                IRelationalRepository<ClassroomTeacher> classTeachersRepository
                                ) : base(classroomsRepository)
        {
            _repository = classroomsRepository;
            _studentsService = studentsService;
            _teachersService = teachersService;
            _classStudentsRepository = classStudentsRepository;
            _classTeachersRepository = classTeachersRepository;
        }
        
        public CreatedEntityDTO Create(string name)
        {
            var classroom = new Classroom(name);
            _repository.Add(classroom);
            return new CreatedEntityDTO(classroom.Id);
        }

        public string AddStudent(Guid id, Guid classId)
        {
            var student = _studentsService.Get(id);
            if (student == null) { return "Student not found"; }
            var classroom = _repository.Get(classId);
            if (classroom == null) { return "Classroom not found"; }

            if (this.GetStudent(classId, id) != null) { return "Student already in class"; }
            
            var relation = new ClassroomStudent(classId, id);
            _classStudentsRepository.Add(relation);

            return null;
        }

        public string AddTeacher(Guid id, Guid classId)
        {
            var teacher = _teachersService.Get(id);
            if (teacher == null) { return "Teacher not found"; }
            var classroom = _repository.Get(classId);
            if (classroom == null) { return "Classroom not found"; }

            if (this.GetTeacher(classId, id) != null) { return "Teacher already in class"; }
            
            var relation = new ClassroomTeacher(classId, id);
            _classTeachersRepository.Add(relation);

            return null;
        }

        public string AddSubjects(Guid id, string subjects)
        {
            var newClass = _repository.Get(id);
            if (newClass == null) { return "Classroom not found"; }

            if (String.IsNullOrEmpty(subjects)) { return "Invalid subjects"; }

            newClass.Subjects += subjects;

            _repository.Modify(id, newClass);

            return null;
        }

        public Student GetStudent(Guid classId, Guid studentId)
        {
            var classroom = _repository.Get(classId);
            if (classroom == null) { return null; }

            var student = classroom.Students.FirstOrDefault(x => x.StudentId == studentId);
            if (student == null) { return null; }

            return student.Student;
        }

        public Teacher GetTeacher(Guid classId, Guid teacherId)
        {
            var classroom = _repository.Get(classId);
            if (classroom == null) { return null; }

            var teacher = classroom.Teachers.FirstOrDefault(x => x.TeacherId == teacherId);
            if (teacher == null) { return null; }

            return teacher.Teacher;
        }
    }
}