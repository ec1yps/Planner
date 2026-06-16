using System.ComponentModel.DataAnnotations;

namespace Planner_server.DTOs
{
	public class CreateTaskRequest
	{
		[Required]
		[MaxLength(200)]
		public string? Title { get; set; }

		[MaxLength(2000)]
		public string? Description { get; set; }

		public DateTime? DueDate { get; set; }
	}
}
