using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Planner_UI.DTOs;
using Planner_UI.Models;
using Planner_UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

		[ObservableProperty]
		private DateTimeOffset? dueDate;

		public TaskViewModel()
		{
			_ = LoadTasksAsync();
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

		[RelayCommand]
		private async Task AddTask()
		{
			if (string.IsNullOrWhiteSpace(Title))
				return;

			CreateTaskRequest request = new()
			{
				Title = Title,
				Description = Description,
				DueDate = DueDate?.LocalDateTime 
			};

			TaskItem? createdTask =
				await ServiceLocator.TaskService.CreateTaskAsync(request);

			if (createdTask is null)
				return;

			Tasks.Add(createdTask);

			Title = string.Empty;
			Description = string.Empty;
		}

		[RelayCommand]
		private async Task ToggleCompleted(TaskItem task)
		{
			UpdateTaskRequest request = new()
			{
				Title = task.Title,
				Description = task.Description,
				DueDate = DueDate?.LocalDateTime,
				IsCompleted = task.IsCompleted
			};

			bool success =
				await ServiceLocator.TaskService.UpdateTaskAsync(task.Id, request);

			if (!success)
				task.IsCompleted = !task.IsCompleted;
		}

		[RelayCommand]
		private void EditTask(TaskItem task)
		{
			task.IsEditing = true;
		}

		[RelayCommand]
		private async Task SaveTask(TaskItem task)
		{
			UpdateTaskRequest request = new()
			{
				Title = task.Title,
				Description = task.Description,
				DueDate = DueDate?.LocalDateTime,
				IsCompleted = task.IsCompleted
			};

			bool success =
				await ServiceLocator.TaskService.UpdateTaskAsync(task.Id, request);
			
			if (success)
				task.IsEditing = false;
		}

		[RelayCommand]
		private void CancelEdit(TaskItem task)
		{
			task.IsEditing = false;
		}

		[RelayCommand]
		private async Task DeleteTask(TaskItem task)
		{
			bool success = await ServiceLocator
				.TaskService
				.DeleteTaskAsync(task.Id);

			if (success)
				Tasks.Remove(task);
		}
	}
}
