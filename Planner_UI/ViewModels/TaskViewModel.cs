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
		private List<TaskItem> _allTasks = [];

		[ObservableProperty]
		private string title = string.Empty;

		[ObservableProperty]
		private string description = string.Empty;

		[ObservableProperty]
		private DateTimeOffset? dueDate;

		[ObservableProperty]
		private TaskFilter selectedFilter;

		public List<TaskFilter> Filters { get; } =
			[
				TaskFilter.All,
				TaskFilter.Active,
				TaskFilter.Completed,
				TaskFilter.Overdue
			];

		public TaskViewModel()
		{
			_ = LoadTasksAsync();
		}

		private async Task LoadTasksAsync()
		{
			List<TaskItem>? tasks =
				await ServiceLocator.TaskService.GetTasksAsync();

			_allTasks = tasks.ToList();
			ApplyFilter();
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

			_allTasks.Add(createdTask);
			ApplyFilter();

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

			ApplyFilter();

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
			{
				_allTasks.Remove(task);
				ApplyFilter();
			}
		}

		private void ApplyFilter()
		{
			IEnumerable<TaskItem> filtered =
			SelectedFilter switch
			{
				TaskFilter.Active =>
					_allTasks.Where(t => !t.IsCompleted),

				TaskFilter.Completed =>
					_allTasks.Where(t => t.IsCompleted),

				TaskFilter.Overdue =>
					_allTasks.Where(t =>
						!t.IsCompleted &&
						t.DueDate.HasValue &&
						t.DueDate.Value.Date < DateTime.Today),

				_ => _allTasks
			};

			Tasks.Clear();

			foreach (TaskItem task in filtered)
				Tasks.Add(task);
		}

		partial void OnSelectedFilterChanged(TaskFilter value)
		{
			ApplyFilter();
		}
	}
}
