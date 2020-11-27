using Microsoft.EntityFrameworkCore;

namespace Domain.Infra
{
    public class TeachContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;User Id=sa;PWD=senha?BOA!;Initial Catalog=Teach");
        }
    }
}