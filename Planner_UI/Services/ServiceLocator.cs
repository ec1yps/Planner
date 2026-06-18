using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner_UI.Services
{
	public static class ServiceLocator
	{
		public static ApiService ApiService { get; } = new();

		public static AuthService AuthService { get; } = new(ApiService);
	}
}
