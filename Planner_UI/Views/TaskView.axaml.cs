using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Planner_UI.ViewModels;

namespace Planner_UI;

public partial class TaskView : Window
{
    public TaskView()
    {
        InitializeComponent();
        DataContext = new TaskViewModel();
    }
}