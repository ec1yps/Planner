using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner_UI.Models
{
	public partial class TaskItem : ObservableObject
	{
		[ObservableProperty]
		private int id;

		[ObservableProperty]
		private string? title;

		[ObservableProperty]
		private string? description;

		[ObservableProperty]
		private DateTime? dueDate;

		[ObservableProperty]
		private bool isCompleted;

		[ObservableProperty]
		private bool isEditing;
	}
}
