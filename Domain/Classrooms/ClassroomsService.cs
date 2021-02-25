using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Grades;
using Domain.StudentPresences;
using Domain.Students;
using Domain.Teachers;
using Domain.MailServices;

namespace Domain.Classrooms
{
    public class ClassroomsService : Service<Classroom>, IClassroomsService
    {
        protected readonly IClassroomsRepository _repository;
        protected readonly IRepository<ClassroomStudent> _classStudentsRepository;
        protected readonly IRepository<ClassroomTeacher> _classTeachersRepository;
        protected readonly IStudentPresencesRepository _presencesRepository;
        protected readonly IStudentsService _studentsService;
        protected readonly ITeachersService _teachersService;
        protected readonly IGradesRepository _gradesRepository;

        public ClassroomsService(IClassroomsRepository classroomsRepository,
                                IStudentsService studentsService,
                                ITeachersService teachersService,
                                IRepository<ClassroomStudent> classStudentsRepository,
                                IRepository<ClassroomTeacher> classTeachersRepository,
                                IStudentPresencesRepository presencesRepository,
                                IGradesRepository gradesRepository
                                ) : base(classroomsRepository)
        {
            _repository = classroomsRepository;
            _studentsService = studentsService;
            _teachersService = teachersService;
            _classStudentsRepository = classStudentsRepository;
            _classTeachersRepository = classTeachersRepository;
            _presencesRepository = presencesRepository;
            _gradesRepository = gradesRepository;
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
            var classroom = _repository.Get(x => x.Id == classId);
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
            var classroom = _repository.Get(x => x.Id == classId);
            if (classroom == null) { return "Classroom not found"; }

            if (this.GetTeacher(classId, id) != null) { return "Teacher already in class"; }
            
            var relation = new ClassroomTeacher(classId, id);
            _classTeachersRepository.Add(relation);

            return null;
        }

        public string SetPresences(Guid classId, List<StudentPresence> presences)
        {
            var classroom = _repository.Get(x => x.Id == classId);
            if (classroom == null) { return "Classroom not found"; }

            var foundPresence = _presencesRepository.Get(x => x.ClassroomId == classId);
            if (foundPresence != null)
            {
                foreach(var p in presences)
                {
                    var entity = _presencesRepository.Get(x => x.StudentId == p.StudentId && x.ClassroomId == p.ClassroomId);
                    _presencesRepository.Remove(entity);
                }
            }
            
            classroom.StudentPresences = presences;
            
            foreach(var p in presences)
            {
                _presencesRepository.Add(p);
                var student = _studentsService.Get(p.StudentId);
                if (student.ParentId != null)
                {
                    var mailService = new MailService();
                    mailService.Send(MailServices.Templates.TemplateType.Absence, student.Parent);
                }
            }

            return null;
        }

        public Student GetStudent(Guid classId, Guid studentId)
        {
            var classroom = _repository.Get(x => x.Id == classId);
            if (classroom == null) { return null; }

            var student = _classStudentsRepository.Get(x => x.StudentId == studentId && x.ClassroomId == classId);
            if (student == null) { return null; }

            return _studentsService.Get(student.StudentId);
        }

        public IList<Student> GetStudents(Guid classId)
        {
            var classroom = _repository.Get(x => x.Id == classId);
            if (classroom == null) { return null; }

            var relations = _classStudentsRepository.GetAll(x => x.ClassroomId == classroom.Id);
            
            var students = _studentsService.GetAll(x => relations.Any(y => y.StudentId == x.Id)).ToList();

            return students;
        }

        public IList<Grade> GetGrades(Guid classId)
        {
            var classroom = _repository.Get(x => x.Id == classId);
            if (classroom == null) { return null; }

            return _gradesRepository.GetAll(x => x.ClassroomId == classroom.Id).ToList();
        }

        public Teacher GetTeacher(Guid classId, Guid teacherId)
        {
            var teacher = _classTeachersRepository.Get(x => x.TeacherId == teacherId && x.ClassroomId == classId);
            if (teacher == null) { return null; }

            return _teachersService.Get(teacher.TeacherId);
        }

        public IList<Teacher> GetTeachers(Guid classId)
        {
            var classroom = _repository.Get(x => x.Id == classId);
            if (classroom == null) { return null; }

            var relations = _classTeachersRepository.GetAll(x => x.ClassroomId == classroom.Id);

            var teachers = _teachersService.GetAll(x => relations.Any(y => y.TeacherId == x.Id)).ToList();

            return teachers;
        }

        public IList<Classroom> GetByTeacher(Guid id)
        {
            var teacher = _teachersService.Get(id);
            if (teacher == null) { return null; }

            var relations = _classTeachersRepository.GetAll(x => x.TeacherId == teacher.Id);

            return _repository.GetAll(x => relations.Any(y => y.ClassroomId == x.Id)).ToList();
        }
    }
}