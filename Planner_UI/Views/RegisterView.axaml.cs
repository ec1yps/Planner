using Avalonia.Controls;
using Planner_UI.ViewModels;

namespace Planner_UI;

public partial class RegisterView : Window
{
    public RegisterView()
    {
        InitializeComponent();
        DataContext = new RegisterViewModel();
	}
}