using System.ComponentModel.DataAnnotations;

namespace Planner_server.DTOs
{
	public class RegisterRequest
	{
		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		public string? Username { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(100)]
		public string? Email { get; set; }

		[Required]
		[MinLength(8)]
		public string? Password { get; set; }
	}
}
