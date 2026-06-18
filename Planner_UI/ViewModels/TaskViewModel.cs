using CommunityToolkit.Mvvm.ComponentModel;
using Planner_UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner_UI.ViewModels
{
	public partial class TaskViewModel : ViewModelBase
	{
		public ObservableCollection<TaskItem> Tasks { get; } = [];

		[ObservableProperty]
		private string title = string.Empty;

		[ObservableProperty]
		private string description = string.Empty;
	}
}
