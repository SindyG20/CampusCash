using Campus.Api.Data;
using Campus.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Campus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _db;
    public StudentsController(AppDbContext db) => _db = db;

    // GET: /api/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAll() =>
        await _db.Students.OrderByDescending(s => s.Id).ToListAsync();

    // GET: /api/students/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Student>> Get(int id)
    {
        var student = await _db.Students.FindAsync(id);
        return student is null ? NotFound() : student;
    }

    // POST: /api/students
    [HttpPost]
    public async Task<ActionResult<Student>> Create(Student dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        _db.Students.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    // PUT: /api/students/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Student dto)
    {
        if (id != dto.Id) return BadRequest("ID mismatch.");
        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: /api/students/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Students.FindAsync(id);
        if (entity is null) return NotFound();
        _db.Students.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}