using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DAL
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
