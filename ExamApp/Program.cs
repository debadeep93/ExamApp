using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Context;
using ExamApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        builder.Services.AddTransient<IStudentsService, StudentsService>();

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
