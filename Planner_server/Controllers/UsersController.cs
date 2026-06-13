using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner_server.Data;
using Planner_server.Models;

namespace Planner_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
	private readonly AppDbContext _context;

	public UsersController(AppDbContext context) => _context = context;

	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> GetAll()
	{
		return await _context.Users.ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<User>> GetById(int id)
	{
		User? user = await _context.Users.FindAsync(id);

		if (user == null) return NotFound();

		return user;
	}

	[HttpPost]
	public async Task<ActionResult<User>> Create(User user)
	{
		_context.Users.Add(user);

		await _context.SaveChangesAsync();

		return CreatedAtAction(
			nameof(GetById), 
			new { id = user.Id }, 
			user);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, User user)
	{
		if(id != user.Id) return BadRequest();

		_context.Entry(user).State = EntityState.Modified;

		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		User? user = await _context.Users.FindAsync(id);

		if (user == null) return NotFound();

		_context.Users.Remove(user);

		await _context.SaveChangesAsync();

		return NoContent();
	}
}