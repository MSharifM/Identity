using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Identity.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {

        }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
