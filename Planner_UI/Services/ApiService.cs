using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Planner_UI.Services
{
	public class ApiService
	{
		public HttpClient HttpClient { get; }

		public ApiService() 
		{
			HttpClient = new()
			{
				BaseAddress = new Uri("https://localhost:7183")
			};
		}

		public void SetToken(string token)
		{
			HttpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
