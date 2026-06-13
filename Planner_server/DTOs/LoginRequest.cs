using System.ComponentModel.DataAnnotations;

namespace Planner_server.DTOs
{
	public class LoginRequest
	{
		[Required]
		public string? Username { get; set; }
		
		[Required]
		public string? Password { get; set; }
	}
}
