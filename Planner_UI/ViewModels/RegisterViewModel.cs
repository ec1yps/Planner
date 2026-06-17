using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Planner_UI.ViewModels
{
	public partial class RegisterViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string username = string.Empty;

		[ObservableProperty]
		private string password = string.Empty;

		[ObservableProperty]
		private string confirmPassword = string.Empty;

		[RelayCommand]
		private void Register()
		{
		}

		[RelayCommand]
		private void Back()
		{
		}
	}
}
