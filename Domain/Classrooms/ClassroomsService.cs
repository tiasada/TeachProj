using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Grades;
using Domain.Students;
using Domain.Teachers;

namespace Domain.Classrooms
{
    public class ClassroomsService : Service<Classroom>, IClassroomsService
    {
        protected readonly IClassroomsRepository _repository;
        protected readonly IRepository<ClassroomStudent> _classStudentsRepository;
        protected readonly IRepository<ClassroomTeacher> _classTeachersRepository;
        protected readonly IStudentsService _studentsService;
        protected readonly ITeachersService _teachersService;
        protected readonly IGradesRepository _gradesRepository;

        public ClassroomsService(IClassroomsRepository classroomsRepository,
                                IStudentsService studentsService,
                                ITeachersService teachersService,
                                IRepository<ClassroomStudent> classStudentsRepository,
                                IRepository<ClassroomTeacher> classTeachersRepository,
                                IGradesRepository gradesRepository
                                ) : base(classroomsRepository)
        {
            _repository = classroomsRepository;
            _studentsService = studentsService;
            _teachersService = teachersService;
            _classStudentsRepository = classStudentsRepository;
            _classTeachersRepository = classTeachersRepository;
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

        public string AddSubject(Guid id, string subject)
        {
            var classroom = _repository.Get(x => x.Id == id);
            if (classroom == null) { return "Classroom not found"; }

            if (String.IsNullOrEmpty(subject)) { return "Invalid subjects"; }

            classroom.Subjects.Add(subject);

            _repository.Modify(classroom);

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
            IList<Student> students = new List<Student>();
            foreach (var item in relations)
            {
                students.Add(_studentsService.Get(x => x.Id == item.StudentId));
            }

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
            var classroom = _repository.Get(x => x.Id == classId);
            if (classroom == null) { return null; }

            var teacher = _classTeachersRepository.Get(x => x.TeacherId == teacherId && x.ClassroomId == classId);
            if (teacher == null) { return null; }

            return _teachersService.Get(teacher.TeacherId);
        }

        public IList<Classroom> GetByTeacher(Guid id)
        {
            var teacher = _teachersService.Get(id);
            if (teacher == null) { return null; }

            var relations = _classTeachersRepository.GetAll(x => x.TeacherId == teacher.Id);
            IList<Classroom> classrooms = new List<Classroom>();
            foreach (var item in relations)
            {
                classrooms.Add(_repository.Get(x => x.Id == item.ClassroomId));
            }

            return classrooms;
        }
    }
}