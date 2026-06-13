using Microsoft.EntityFrameworkCore;
using Planner_server.Models;

namespace Planner_server.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<TaskItem> TaskItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Username)
				.IsUnique();

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();

			modelBuilder.Entity<TaskItem>()
				.HasOne(t => t.User)
				.WithMany(u => u.Tasks)
				.HasForeignKey(t => t.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
