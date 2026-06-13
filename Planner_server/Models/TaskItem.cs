using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner_server.Models
{
	public class TaskItem
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(200)]
		public string? Title { get; set; }

		[MaxLength(2000)]
		public string? Description { get; set; }

		public bool IsCompleted { get; set; } = false;

		[Required]
		public int UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		public User? User { get; set; }
	}
}
