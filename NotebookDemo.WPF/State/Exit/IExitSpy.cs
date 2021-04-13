using System;

namespace NotebookDemo.WPF.State.Exit
{
	/// <summary>
	/// Defines functionality for requesting and handling the app exit.
	/// </summary>
	public interface IExitSpy
	{
		/// <summary>
		/// Exit event.
		/// </summary>
		event Action Exit;

		/// <summary>
		/// Exit request event.
		/// </summary>
		event Action ExitRequest;

		/// <summary>
		/// Invokes the <see cref="Exit"/>.
		/// </summary>
		void OnExit();

		/// <summary>
		/// Invokes the <see cref="ExitRequest"/>.
		/// </summary>
		void OnExitRequest();
	}
}