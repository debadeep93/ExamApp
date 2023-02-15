using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Context;
using ExamApp.Interfaces;
using ExamApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExamApp;

public class Program
{
    public static void Main(string[] args)
    {
        // Uncomment for dev
        //var db = new MainContext();

        //for (var i = 0; i < 10; i++)
        //{
        //    db.Languages.Add(new Language(Guid.NewGuid(), $"Lang {i}"));
        //}

        //db.SaveChanges();

        CreateApp(args).Run();
    }

    public static WebApplication CreateApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<MainContext>(options =>
        {
            if (builder.Environment.IsDevelopment())
            {
                options.UseInMemoryDatabase("Dev");
            }
            else
            {
                var connectionString = builder.Configuration.GetValue<string>("DbConnectionString");
                options.UseSqlServer(connectionString);
            }
        });
        //builder.Services.AddTransient<IStudentsService, StudentsService>();
        builder.Services.AddScoped<IStudentsService, StudentsService>();
        builder.Services.AddScoped<ICoursesService, CourseService>();
        // Service should be scoped instead, as we want to maintain the context throughout the lifetime of a single http request, and not a single initialization

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.Map("languages", (LanguageService service) => service.GetLanguages());

        return app;
    }
}
