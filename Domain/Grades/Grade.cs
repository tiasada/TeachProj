using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Classrooms;
using Domain.Entities;

namespace Domain.Grades
{
    public class Grade : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsClosed { get; set; }
        public virtual Classroom Classroom { get; set; }
        public Guid ClassroomId { get; set; }

        public Grade(string name, string description, DateTime date, Classroom classroom)
        {
            Name = name;
            Description = description;
            Classroom = classroom;
            ClassroomId = classroom.Id;
            Date = date.Date;
        }

        protected Grade(){}

        // public bool SetGrade(Guid studentId, double grade)
        // {
        //     if (Grades.Any(x => x.studentId == studentId))
        //     {
        //         Grades.Select(x => x.studentId == studentId ? (x.studentId, grade) : x).ToList();
        //         return true;
        //     }
        //     return false;
        // }
    }
}