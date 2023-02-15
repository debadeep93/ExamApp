using ExamApp.Domain.Entities;
using ExamApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExamApp.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentsService _service;

    public StudentsController(IStudentsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _service.GetAllAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // Missing ID param in the HttpGet annotation would lead to the application not being able to map the endpoints correctly
    [HttpGet("{id}")]
    public async Task< IActionResult> Get([FromRoute] int id) // From route annotation to denote that the id must be specified in the route.
    {
        try
        {
            return Ok(await _service.GetAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Student student)
    {
        try
        {
            // Validate if student exists (duplicate check by id)
            Student existing = await _service.GetAsync(student.Id);
            if(existing != null)
            {
                return Conflict(student.Id);
            }

            if(student.Age < 18)
            {
                return BadRequest($"Age specified: {existing.Age} too low for registration");
            }

            await _service.AddAsync(student);

            return Created("[controller]/" + student.Id, student);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")] // Put
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] Student student)
    {
        try
        {
            Student existing = await _service.GetAsync(id);
            if(existing == null)
            {
                return NotFound(id);
            }

            await _service.UpdateAsync(student);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}