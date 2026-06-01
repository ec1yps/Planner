using Microsoft.EntityFrameworkCore;
using Planner_server.Models;

namespace Planner_server.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<TaskItem> TaskItems { get; set; }
	}
}
