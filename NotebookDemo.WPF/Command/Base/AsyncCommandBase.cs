using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NotebookDemo.WPF.State.Exit;

namespace NotebookDemo.WPF.Command.Base
{
	/// <summary>
	/// Abstract <see cref="ICommand"/> implementation.<br/>
	/// </summary>
	public abstract class AsyncCommandBase : ICommand
	{
		#region Private Members

		private readonly IErrorSpy _errorSpy;

		#endregion

		#region Public Properties

		private bool _isExecuting;
		public bool IsExecuting
		{
			get
			{
				return _isExecuting;
			}
			set
			{
				_isExecuting = value;
				OnCanExecuteChanged();
			}
		}

		#endregion

		#region Public Events

		public event EventHandler CanExecuteChanged;

		#endregion


		#region Constructor

		public AsyncCommandBase(IErrorSpy errorSpy)
		{
			_errorSpy = errorSpy;
		}

		#endregion

		#region Public Methods

		public virtual bool CanExecute(object parameter) => IsExecuting == false;

		public async void Execute(object parameter)
		{
			IsExecuting = true;
			try
			{
				await ExecuteAsync();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
			finally
			{
				IsExecuting = false;
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

		/// <summary>
		/// The task to execute.
		/// </summary>
		protected abstract Task ExecuteAsync();

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
