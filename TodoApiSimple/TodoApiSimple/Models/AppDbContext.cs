using Microsoft.EntityFrameworkCore;

namespace TodoApiSimple.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=EN00398\\SQLEXPRESS;Initial Catalog=todoapisimple;Integrated Security=True");
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
