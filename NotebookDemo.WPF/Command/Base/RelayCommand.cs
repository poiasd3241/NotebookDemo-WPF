using System;
using System.Windows.Input;

namespace NotebookDemo.WPF.Command.Base
{
	/// <summary>
	/// Custom <see cref="ICommand"/> implementation.<br/>
	/// Any parameter passed in to the <see cref="CanExecute(object)"/> or <see cref="Execute(object)"/> will be ignored.
	/// </summary>
	public class RelayCommand : ICommand
	{
		#region Private Members

		/// <summary>
		/// The action to execute.
		/// </summary>
		private readonly Action _execute;

		/// <summary>
		/// Whether the command can execute.
		/// </summary>
		private readonly Func<bool> _canExecute;

		#endregion

		#region Public Events

		public event EventHandler CanExecuteChanged;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor to use when the command should always be able to execute.
		/// </summary>
		/// <param name="execute">The action to execute.</param>
		public RelayCommand(Action execute) : this(execute, null)
		{
			_execute = execute;
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="execute">The action to execute.</param>
		/// <param name="canExecute">Whether the command can execute.</param>
		public RelayCommand(Action execute, Func<bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		#endregion

		#region Public Methods

		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute();
		}

		public void Execute(object parameter)
		{
			_execute();
		}

		/// <summary>
		/// Shortcut for the command execution.
		/// </summary>
		public void Execute()
		{
			Execute(null);
		}

		public void OnCanExecuteChanged(object sender = null, EventArgs e = null)
		{
			CanExecuteChanged?.Invoke(sender, e);
		}

		#endregion
	}
}
