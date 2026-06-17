using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Planner_UI.Services
{
	internal class ApiService
	{
		public HttpClient HttpClient { get; }
		public ApiService() 
		{
			HttpClient = new()
			{
				BaseAddress = new Uri("https://localhost:5295/api/")
			};
		}
	}
}
