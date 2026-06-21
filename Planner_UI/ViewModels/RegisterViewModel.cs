using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Planner_UI.DTOs;
using Planner_UI.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Planner_UI.ViewModels
{
	public partial class RegisterViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string username = string.Empty;

		[ObservableProperty]
		private string email = string.Empty;

		[ObservableProperty]
		private string password = string.Empty;

		[ObservableProperty]
		private string errorMessage = string.Empty;

		[RelayCommand]
		private async Task Register()
		{
			var request = new RegisterRequest
			{
				Username = Username,
				Email = Email,
				Password = Password
			};

			if (request.Password.Length < 8)
			{
				ErrorMessage = "Password must be at least 8 characters long.";
				return;
			}
			if (!Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
			{
				ErrorMessage = "Invalid email format.";
				return;
			}

			bool success = await ServiceLocator.AuthService.RegisterAsync(request);

			if (success)
			{
				LoginView loginWindow = new();
				loginWindow.Show();

				if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
				{
					desktop.MainWindow?.Close();
					desktop.MainWindow = loginWindow;
				}
			}
		}

		[RelayCommand]
		private void Back()
		{
			LoginView loginWindow = new();
			loginWindow.Show();

			if(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				desktop.MainWindow?.Close();
				desktop.MainWindow = loginWindow;
			}
		}
	}
}
