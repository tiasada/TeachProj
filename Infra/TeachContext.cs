using System.Reflection;
using Domain.ClassDays;
using Domain.Classrooms;
using Domain.Grades;
using Domain.Students;
using Domain.Teachers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class TeachContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<ClassroomStudent> ClassroomStudents { get; set; }
        public DbSet<ClassroomTeacher> ClassroomTeachers { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
        public DbSet<ClassDay> ClassDays { get; set; }
        public DbSet<StudentPresence> StudentPresences { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;User Id=sa;PWD=senha?BOA!;Initial Catalog=Teach");
        }
    }
}