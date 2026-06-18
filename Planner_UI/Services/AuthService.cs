using Planner_UI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Planner_UI.Services
{
	public class AuthService
	{
		private readonly ApiService _apiService;

		public string? Token { get; private set; }

		public AuthService(ApiService apiService)
		{
			_apiService = apiService;
		}

		public async Task<bool> RegisterAsync(RegisterRequest request)
		{
			var response = await _apiService.HttpClient.PostAsJsonAsync(
				"api/auth/register",
				request);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> LoginAsync(LoginRequest request)
		{
			var response = await _apiService.HttpClient.PostAsJsonAsync(
				"api/auth/login",
				request);

			if (!response.IsSuccessStatusCode)
				return false;

			var authResponse =
				await response.Content.ReadFromJsonAsync<AuthResponse>();

			if (authResponse == null)
				return false;

			Token = authResponse.Token;

			_apiService.SetToken(Token!);

			return true;
		}

		public void Logout()
		{
			Token = null;

			_apiService.HttpClient.DefaultRequestHeaders.Authorization = null;
		}
	}
}
