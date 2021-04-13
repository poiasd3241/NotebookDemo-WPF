using System;
using System.Windows.Input;
using NotebookDemo.WPF.State.Exit;

namespace NotebookDemo.WPF.Command.Base
{
	/// <summary>
	/// Abstract <see cref="ICommand"/> implementation.<br/>
	/// </summary>
	public abstract class CommandBase : ICommand
	{
		#region Private Members

		private readonly IErrorSpy _errorSpy;

		#endregion

		#region Public Events

		public event EventHandler CanExecuteChanged;

		#endregion

		#region Constructor

		public CommandBase(IErrorSpy errorSpy)
		{
			_errorSpy = errorSpy;
		}

		#endregion

		#region Public Methods

		public virtual bool CanExecute(object parameter) => true;

		public void Execute(object parameter)
		{
			try
			{
				SafeExecute();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		/// <summary>
		/// Shortcut for the command execution.
		/// </summary>
		public void Execute()
		{
			Execute(null);
		}

		#endregion

		#region Protected Members

		protected abstract void SafeExecute();
		protected virtual void HandleException(Exception ex)
		{
			_errorSpy.OnError(new UI.ErrorUI.ErrorViewModel(ex));
		}

		protected void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}

		#endregion
	}
}
