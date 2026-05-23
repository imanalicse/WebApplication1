using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var converter = new ValueConverter<string[], string>(
                v => v == null ? "[]" : JsonSerializer.Serialize(v),
                v => string.IsNullOrEmpty(v) ? new string[0] : JsonSerializer.Deserialize<string[]>(v) ?? new string[0]);

            modelBuilder.Entity<Book>().Property(b => b.Tags).HasConversion(converter);
        }
    }
}
