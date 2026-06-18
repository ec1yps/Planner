using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Planner_UI.Services;
using Planner_UI.DTOs;
using System.Threading.Tasks;

namespace Planner_UI.ViewModels
{
	public partial class LoginViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string username = string.Empty;

		[ObservableProperty]
		private string password = string.Empty;

		[RelayCommand]
		private async Task Login()
		{
			var request = new LoginRequest
			{
				Username = Username,
				Password = Password
			};

			bool success = await ServiceLocator.AuthService.LoginAsync(request);

			if (success)
			{
				TaskView taskWindow = new();
				taskWindow.Show();

				if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
				{
					desktop.MainWindow?.Close();
					desktop.MainWindow = taskWindow;
				}
			}
			else
			{
				// Показать ошибку
			}
		}

		[RelayCommand]
		private void StartRegistration()
		{
			RegisterView regWindow = new();
			regWindow.Show();

			if(App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				desktop.MainWindow?.Close();
				desktop.MainWindow = regWindow;
			}
		}
	}
}
