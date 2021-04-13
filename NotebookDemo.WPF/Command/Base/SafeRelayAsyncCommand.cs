using System;
using System.Threading.Tasks;
using System.Windows.Input;
using NotebookDemo.WPF.State.Exit;

namespace NotebookDemo.WPF.Command.Base
{
	/// <summary>
	/// Custom <see cref="ICommand"/> implementation with support of crude exception handling.<br/>
	/// Any parameter passed in to the <see cref="CanExecute(object)"/> or <see cref="Execute(object)"/> will be ignored.
	/// </summary>
	public class SafeRelayAsyncCommand : ICommand
	{
		#region Private Members

		private readonly IErrorSpy _errorSpy;

		/// <summary>
		/// The delegate to execute safely.
		/// </summary>
		private readonly Func<Task> _safeExecute;

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
		/// <param name="safeExecute">The delegate to execute safely.</param>
		public SafeRelayAsyncCommand(IErrorSpy errorSpy, Func<Task> safeExecute) : this(errorSpy, safeExecute, null)
		{
			_safeExecute = safeExecute;
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="safeExecute">The delegate to execute safely.</param>
		/// <param name="canExecute">Whether the command can execute.</param>
		public SafeRelayAsyncCommand(IErrorSpy errorSpy, Func<Task> safeExecute, Func<bool> canExecute)
		{
			_errorSpy = errorSpy;
			_safeExecute = safeExecute;
			_canExecute = canExecute;
		}

		#endregion

		#region Public Methods

		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute();
		}

		public async void Execute(object parameter)
		{
			try
			{
				await _safeExecute();
			}
			catch (Exception ex)
			{
				_errorSpy.OnError(new UI.ErrorUI.ErrorViewModel(ex));
			}
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
