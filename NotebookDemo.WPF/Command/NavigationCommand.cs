using NotebookDemo.WPF.Command.Base;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.State.Navigation;

namespace NotebookDemo.WPF.Command
{
	/// <summary>
	/// Used for navigating between views.
	/// </summary>
	public class NavigationCommand : CommandBase
	{
		private readonly NavigatorBase _navigator;
		private readonly object _parameter;
		private readonly ViewType _destination;

		public NavigationCommand(IErrorSpy errorSpy, NavigatorBase navigator, ViewType destination, object parameter = null) : base(errorSpy)
		{
			_navigator = navigator;
			_destination = destination;
			_parameter = parameter;
		}

		protected override void SafeExecute()
		{
			_navigator.GoTo(_destination, _parameter);
		}
	}
}
