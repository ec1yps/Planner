using System.ComponentModel.DataAnnotations;

namespace Planner_server.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string? Username { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(100)]
		public string? Email { get; set; }

		[Required]
		public string? PasswordHash { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public ICollection<TaskItem> Tasks { get; set; } = [];
	}
}
