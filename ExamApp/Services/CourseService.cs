using ExamApp.Context;
using ExamApp.Domain.Entities;
using ExamApp.Interfaces;

namespace ExamApp.Services
{
    public class CourseService : CrudService<Course>, ICoursesService
    {
        public CourseService(MainContext context) : base(context)
        {
        }
    }
}
