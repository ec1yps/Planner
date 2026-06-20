using Planner_UI.DTOs;
using Planner_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
				await _apiService.HttpClient.GetFromJsonAsync<List<TaskItem>>("api/Task");

			return tasks ?? new List<TaskItem>();
		}

		public async Task<TaskItem?> CreateTaskAsync(CreateTaskRequest request)
		{
			HttpResponseMessage response =
				await _apiService.HttpClient.PostAsJsonAsync("api/Task", request);

			if (!response.IsSuccessStatusCode)
				return null;

			return await response.Content.ReadFromJsonAsync<TaskItem>();
		}

		public async Task<bool> UpdateTaskAsync(int id, UpdateTaskRequest request)
		{
			HttpResponseMessage response =
				await _apiService.HttpClient.PutAsJsonAsync($"api/Task/{id}", request);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteTaskAsync(int id)
		{
			HttpResponseMessage response =
				await _apiService.HttpClient.DeleteAsync($"api/Task/{id}");

			return response.IsSuccessStatusCode;
		}
	}
}
