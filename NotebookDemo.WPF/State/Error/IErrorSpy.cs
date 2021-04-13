using System;
using NotebookDemo.WPF.UI.ErrorUI;

namespace NotebookDemo.WPF.State.Exit
{
	/// <summary>
	/// Defines functionality for raising an error that can be consumed by subscribers.
	/// </summary>
	public interface IErrorSpy
	{
		/// <summary>
		/// Handles the error event.
		/// </summary>
		event EventHandler<ErrorViewModel> Error;

		/// <summary>
		/// Invokes the <see cref="Error"/>.
		/// </summary>
		/// <param name="error">The error that occurred.</param>
		void OnError(ErrorViewModel error);
	}
}