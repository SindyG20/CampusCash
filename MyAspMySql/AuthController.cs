using Microsoft.AspNetCore.Mvc;
using MyAspMySql.Data;
using MyAspMySql.Models;
using System.Linq;

namespace MyAspMySql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
public IActionResult Register([FromBody] User userDto)
{
    if (_context.Users.Any(u => u.Email == userDto.Email))
        return BadRequest("Email already exists");

    var user = new User
    {
        StudentId = userDto.StudentId,
        StudentNumber = userDto.StudentNumber,
        FullName = userDto.FullName,
        DateOfBirth = userDto.DateOfBirth,
        Address = userDto.Address,
        ContactNumber = userDto.ContactNumber,
        AccountType = userDto.AccountType,
        Email = userDto.Email,
        PasswordHash = userDto.PasswordHash, // ⚠ hash in real apps
        Role = "Student",
        CreatedAt = DateTime.Now
        
    };

    _context.Users.Add(user);
    _context.SaveChanges();

    var account = new Account
    {
        UserId = user.Id,
        AccountNumber = new Random().Next(10000000, 99999999).ToString(),
        Balance = 0,
        CreatedAt = DateTime.Now
         };

    _context.Accounts.Add(account);
    _context.SaveChanges();

    return Ok("Registration successful");
}

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginDto)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Email == loginDto.Email && u.PasswordHash == loginDto.PasswordHash);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var account = _context.Accounts.FirstOrDefault(a => a.UserId == user.Id);

            return Ok(new
            {
                user.Username,
                user.Email,
                Balance = account?.Balance ?? 0,
                user.DateOfBirth,
                user.CreatedAt,
                user.Address
            });
        }

        

    }
}