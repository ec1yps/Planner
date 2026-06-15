using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Planner_server.Data;
using Planner_server.DTOs;
using Planner_server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Planner_server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IConfiguration _configuration;

		public AuthController(AppDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
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

			if (emailExists)
				return BadRequest("Email already exists");

			User user = new()
			{
				Username = request.Username,
				Email = request.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
			};

			_context.Users.Add(user);

			await _context.SaveChangesAsync();

			return Ok("User registered successfully");
		}

		[HttpPost("login")]
		public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(
				u => u.Username == request.Username);

			if (user == null)
				return Unauthorized("Invalid username");

			bool validPassword = BCrypt.Net
				.BCrypt.Verify(request.Password, user.PasswordHash);

			if (!validPassword)
				return Unauthorized("Invalid password");

			string token = GenerateToken(user);

			return Ok(new AuthResponse { Token = token });
		}

		private string GenerateToken(User user)
		{
			Claim[] _claims =
			[
				new Claim(
				ClaimTypes.NameIdentifier,
				user.Id.ToString()),

				new Claim(
					ClaimTypes.Name,
					user.Username!)
			];

			SymmetricSecurityKey key = new(
				Encoding.UTF8.GetBytes(
					_configuration["Jwt:Key"]!));


			SigningCredentials credentials = new(
				key, SecurityAlgorithms.HmacSha256);

			JwtSecurityToken token = new(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: _claims,
				expires: DateTime.UtcNow.AddDays(7),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}