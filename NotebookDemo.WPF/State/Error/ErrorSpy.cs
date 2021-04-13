using System;
using NotebookDemo.WPF.UI.ErrorUI;

namespace NotebookDemo.WPF.State.Exit
{
	/// <summary>
	/// Default implementation of <see cref="IErrorSpy"/>.
	/// </summary>
	public class ErrorSpy : IErrorSpy
	{
		public event EventHandler<ErrorViewModel> Error;

		public void OnError(ErrorViewModel error)
		{
			Error?.Invoke(null, error);
		}
	}
}
