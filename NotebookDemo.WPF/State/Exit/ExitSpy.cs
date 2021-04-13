using System;

namespace NotebookDemo.WPF.State.Exit
{
	/// <summary>
	/// Default implementation of <see cref="IExitSpy"/>.
	/// </summary>
	public class ExitSpy : IExitSpy
	{
		#region Public Events

		public event Action Exit;
		public event Action ExitRequest;

		#endregion

		#region Public Methods

		public void OnExit()
		{
			Exit?.Invoke();
		}

		public void OnExitRequest()
		{
			ExitRequest?.Invoke();
		}

		#endregion
	}
}
