using CommunityToolkit.Mvvm.ComponentModel;
using Planner_UI.Models;
using Planner_UI.Services;
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

		public TaskViewModel()
		{
			
		}

		private async Task LoadTasksAsync()
		{
			List<TaskItem>? tasks = 
				await ServiceLocator.TaskService.GetTasksAsync();

			Tasks.Clear();

			foreach (var task in tasks)
			{
				Tasks.Add(task);
			}
		}
	}
}
