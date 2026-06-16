using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Planner_server.Data;
using Planner_server.Models;
using System.Security.Claims;
using Planner_server.DTOs;


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
		public async Task<ActionResult<TaskItem>> Create(CreateTaskRequest request)
		{
			TaskItem task = new()
			{ 
				Title = request.Title,
				Description = request.Description,
				DueDate = request.DueDate,
				UserId = GetUserId()
			};

			_context.TaskItems.Add(task);
			await _context.SaveChangesAsync();

			return CreatedAtAction(
				nameof(GetById), 
				new { id = task.Id }, 
				task);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, UpdateTaskRequest request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			int userId = GetUserId();

			TaskItem? task = await _context.TaskItems
				.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

			if (task == null) return NotFound();

			task.Title = request.Title;
			task.Description = request.Description;
			task.DueDate = request.DueDate;
			task.IsCompleted = request.IsCompleted;

			_context.TaskItems.Update(task);

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
			string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

			return int.Parse(userId!);
		}
	}
}
