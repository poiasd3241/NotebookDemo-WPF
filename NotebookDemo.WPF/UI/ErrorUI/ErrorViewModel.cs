using System;
using System.Windows.Input;
using NotebookDemo.WPF.Command.Base;

namespace NotebookDemo.WPF.UI.ErrorUI
{
	public class ErrorViewModel : ViewModelBase
	{
		#region Private Members

		private readonly Exception _exception;

		#endregion

		#region Public Properties

		public string Title { get; }
		public string Message => _exception.Message;
		public string StackTrace => _exception.StackTrace ?? "No stack trace.";
		public bool HasStackTrace => StackTrace != null;
		public string ExceptionName => _exception.GetType().Name;
		public ICommand ExitCommand { get; private set; }

		public event Action Exit;

		#endregion

		#region Constructors

		/// <summary>
		/// Parameterless constructor for XAML designer.
		/// </summary>
		public ErrorViewModel()
		{ }

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="exception">The exception that is to be represented by this view model.</param>
		public ErrorViewModel(Exception exception)
		{
			Title = "Error occurred";
			_exception = exception;
			ExitCommand = new RelayCommand(() => Exit?.Invoke());
		}

		#endregion
	}
}
