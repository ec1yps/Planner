using Planner_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Planner_UI.Services
{
	public class TaskService
	{
		private readonly ApiService _apiService;

		public TaskService(ApiService apiService)
		{
			_apiService = apiService;
		}

		public async Task<List<TaskItem>> GetTasksAsync()
		{
			List<TaskItem>? tasks =
				await _apiService.HttpClient.GetFromJsonAsync<List<TaskItem>>("api/task");

			return tasks ?? new List<TaskItem>();
		}
	}
}
