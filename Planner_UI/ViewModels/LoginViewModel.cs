using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Planner_UI.ViewModels
{
	public partial class LoginViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string username = string.Empty;

		[ObservableProperty]
		private string password = string.Empty;

		[RelayCommand]
		private void Login()
		{
		}

		[RelayCommand]
		private void StartRegistration()
		{
		}
	}
}
