using BlazorBookManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; } = default!;
    }
}
