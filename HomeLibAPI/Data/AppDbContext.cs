using HomeLibAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLibAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<BookModel> BookModels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
