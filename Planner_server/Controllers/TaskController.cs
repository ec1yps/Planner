using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Planner_server.Data;
using Planner_server.Models;
using System.Security.Claims;


namespace Planner_server.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]")]
	public class TaskController : ControllerBase
	{
		private readonly AppDbContext _context;

		public TaskController(AppDbContext context) => _context = context;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
		{
			int userId = GetUserId();

			return await _context.TaskItems
				.Where(t => t.UserId == userId)
				.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TaskItem>> GetById(int id)
		{
			int userId = GetUserId();

			TaskItem? task = await _context.TaskItems
				.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

			if (task == null) return NotFound();

			return task;
		}

		[HttpPost]
		public async Task<ActionResult<TaskItem>> Create(TaskItem task)
		{
			bool userExist = await _context.Users
				.AnyAsync(u => u.Id == task.UserId);

			if (!userExist) return BadRequest("User does not exist.");

			task.UserId = GetUserId();

			_context.TaskItems.Add(task);
			await _context.SaveChangesAsync();

			return CreatedAtAction(
				nameof(GetById), 
				new { id = task.Id }, 
				task);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, TaskItem task)
		{
			int userId = GetUserId();

			TaskItem? existingTask = await _context.TaskItems
				.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

			if (existingTask == null) return NotFound();

			_context.Entry(task).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			int userId = GetUserId();

			TaskItem? task = await _context.TaskItems
				.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

			if (task == null) return NotFound();
			_context.TaskItems.Remove(task);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private int GetUserId()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			return int.Parse(userId!);
		}
	}
}
