using Microsoft.EntityFrameworkCore;
using TestLatviaProject.Models;

namespace TestLatviaProject.Data
{
    public class TestDBContext : DbContext
    {
        public TestDBContext(DbContextOptions<TestDBContext> options) : base(options) { }
        
        public DbSet<Tasks> Tasks { get; set; }
    }
}
