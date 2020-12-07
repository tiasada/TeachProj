using System.Reflection;
using Domain.Classrooms;
using Domain.StudentClassrooms;
using Domain.Students;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infra
{
    public class TeachContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<StudentClassroom> StudentClassrooms { get; set; }
        
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