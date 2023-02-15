using ExamApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Context;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasMany(d => d.Students)
                .WithMany(p => p.Courses);

            entity.HasOne(d => d.Language);
        });
    }
}