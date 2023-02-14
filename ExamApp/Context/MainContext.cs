using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Context;

public class MainContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Uncomment for dev
        //optionsBuilder.UseInMemoryDatabase("Dev");

        // Comment for dev
        optionsBuilder.UseSqlServer(
            "server=production.server.com;database=mainDB;trusted_connection=true;username=secureadmin;password=verytrustedpa$$word123");
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasMany(d => d.Students)
                .WithOne(p => p.Course)
                .HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.Language);
        });
    }
}

public class Course
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Student> Students { get; set; }
    public virtual Language Language { get; set; }
}

public class Student
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Guid CourseId { get; set; }

    public virtual Course Course { get; set; }
}

public record Language(Guid Id, string Title);