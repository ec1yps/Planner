using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner_UI.DTOs
{
	public class CreateTaskRequest
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
	}
}
