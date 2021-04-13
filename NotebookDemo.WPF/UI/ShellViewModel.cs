using NotebookDemo.WPF.Command;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.State.Navigation;
using NotebookDemo.WPF.UI.ErrorUI;

namespace NotebookDemo.WPF.UI
{
	/// <summary>
	/// Shell of the application. Controls time speed.
	/// </summary>
	public class ShellViewModel : ViewModelBase
	{
		#region Private Members

		private readonly MainNavigator _mainNavigator;
		private readonly IErrorSpy _errorSpy;
		private readonly IExitSpy _exitSpy;

		#endregion

		#region Public Properties

		public ViewModelBase CurrentViewModel => _mainNavigator?.CurrentViewModel;

		private ErrorViewModel _error;
		public ErrorViewModel Error
		{
			get => _error;
			set
			{
				if (_error == null && value != null)
				{
					_error = value;
					Error.Exit += Error_Exit;
					OnPropertyChanged();
					OnPropertyChanged(nameof(HasError));
				}
			}
		}
		public bool HasError => Error != null;

		#endregion

		#region Constructors

		/// <summary>
		/// Parameterless constructor for XAML designer.
		/// </summary>
		public ShellViewModel()
		{ }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ShellViewModel(MainNavigator mainNavigator, IErrorSpy errorSpy, IExitSpy exitSpy)
		{
			_mainNavigator = mainNavigator;
			_errorSpy = errorSpy;
			_exitSpy = exitSpy;
			_errorSpy.Error += ErrorSpy_Error;

			_mainNavigator.CurrentViewModelChanged += MainNavigator_CurrentViewModelChanged;

			new NavigationCommand(errorSpy, mainNavigator, ViewType.Home).Execute();
		}

		#endregion

		#region Private Methods

		private void Error_Exit()
		{
			// Request the app exit after dismissing the error message.
			_exitSpy.OnExitRequest();
		}

		private void ErrorSpy_Error(object sender, ErrorViewModel error)
		{
			Error = error;
		}

		private void MainNavigator_CurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}

		#endregion
	}
}
