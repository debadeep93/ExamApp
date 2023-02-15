using ExamApp.Context;
using ExamApp.Domain.Entities;
using ExamApp.Interfaces;

namespace ExamApp.Services;

public class StudentsService : CrudService<Student>, IStudentsService
{
    public StudentsService(MainContext mainContext) : base(mainContext) { }
}