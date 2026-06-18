using Avalonia.Controls;
using Planner_UI.ViewModels;

namespace Planner_UI;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();
        DataContext = new LoginViewModel();
	}
}