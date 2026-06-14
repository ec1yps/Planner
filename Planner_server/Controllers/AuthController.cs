using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner_server.Data;
using Planner_server.DTOs;
using Planner_server.Models;
namespace Planner_server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : Controller
	{
		private readonly AppDbContext _context;
		public AuthController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			bool usernameExists = await _context.Users
				.AnyAsync(u => u.Username == request.Username);

			if (usernameExists)
				return BadRequest("Username already exists");

			bool emailExists = await _context.Users
				.AnyAsync(u => u.Email == request.Email);

			if(emailExists)
				return BadRequest("Email already exists");

			User user = new User
			{
				Username = request.Username,
				Email = request.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
			};

			_context.Users.Add(user);

			await _context.SaveChangesAsync();

			return Ok("User registered successfully");
		}
	}
}