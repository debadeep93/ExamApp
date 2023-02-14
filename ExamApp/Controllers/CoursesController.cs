using System;
using System.Linq;
using ExamApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers;

public class CoursesController: ControllerBase
{
    private IStudentsService _service;

    public CoursesController(IStudentsService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetCourses());
    }

    [HttpPost]
    public IActionResult AddStudentToCourse(int studentId, Guid courseId)
    {
        try
        {
            var course = _service.GetCourse(courseId);
            var student = _service.GetAllStudents()
                .FirstOrDefault(x => x.Id == studentId);

            if (student == null || student.Age < 18)
            {
                throw new Exception();
            }

            course.Students.Add(student);
            _service.ModifyCourse(courseId, course);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
