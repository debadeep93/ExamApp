using ExamApp.Domain.Entities;
using ExamApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExamApp.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
public class CoursesController: ControllerBase
{

    private readonly ICoursesService _courseService;
    private readonly IStudentsService _studentsService;

    public CoursesController(ICoursesService service, IStudentsService studentsService)
    {
        _courseService = service;
        _studentsService = studentsService;
    }
    // No endpoints to get course by id, create a course, or delete a course.
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _courseService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(Guid id) 
    {
        return Ok(await _courseService.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(Course entity)
    {
        try
        {
            // Validate if student exists (duplicate check by id)
            Course existing = await _courseService.GetAsync(entity.Id);
            if (existing != null)
            {
                return Conflict(existing.Id);
            }
            if (string.IsNullOrWhiteSpace(entity.Title))
            {
                return BadRequest($"{nameof(entity.Title)} can not be null, empty or whitespace");
            }

            await _courseService.AddAsync(entity);

            return Created($"/courses/{entity.Id}", entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] Course course)
    {
        try
        {
            Course existing = await _courseService.GetAsync(id);
            if (existing == null)
            {
                return NotFound(id);
            }

            await _courseService.UpdateAsync(course);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("{courseId}/students")] // not in line with REST API conventions. default POST method should only be used to add a new course. // Put is appropriate
    public async Task<IActionResult> AddStudentToCourseAsync([FromRoute] Guid courseId, [FromBody] Student model)
    {
        try
        {
            var course = await _courseService.GetAsync(courseId);
            var student = await _studentsService.GetAsync(model.Id);
            if(course == null)
            {
                return NotFound($"courseId: {courseId}");
            }
            if (student == null)
            {
                return NotFound($"studentId: {model.Id}"); // Meaningful message required. Otherwise end user/ client has no indication of what went wrong
            }

            course.Students.Add(student);
            await _courseService.UpdateAsync(course);

            return Accepted(); 
        }
        catch (Exception e)
        {
            // No message set in the exception. will not return any information.
            return BadRequest(e.Message);
        }
    }
}
