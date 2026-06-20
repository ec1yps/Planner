using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner_UI.DTOs
{
	public class UpdateTaskRequest
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public DateTime? DueDate { get; set; }
		public bool IsCompleted { get; set; }
	}
}
