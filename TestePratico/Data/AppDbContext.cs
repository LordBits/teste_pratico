using Microsoft.EntityFrameworkCore;
using TestePratico.Models;

namespace TestePratico.Data
{
    public class AppDbContext : DbContext
    {
         public DbSet<Cadeira> Cadeiras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=clinicadentista;user=root;password=;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}