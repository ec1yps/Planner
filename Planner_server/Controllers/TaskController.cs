using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner_server.Data;
using Planner_server.Models;

namespace Planner_server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly AppDbContext _context;

		public TaskController(AppDbContext context) => _context = context;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
		{
			return await _context.TaskItems.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TaskItem>> GetById(int id)
		{
			TaskItem? task = await _context.TaskItems.FindAsync(id);
			if (task == null) return NotFound();

			return task;
		}

		[HttpPost]
		public async Task<ActionResult<TaskItem>> Create(TaskItem task)
		{
			_context.TaskItems.Add(task);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, TaskItem task)
		{
			if (id != task.Id) return BadRequest();

			_context.Entry(task).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			TaskItem? task = await _context.TaskItems.FindAsync(id);
			if (task == null) return NotFound();
			_context.TaskItems.Remove(task);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
