using System;
using NotebookDemo.WPF.UI;

namespace NotebookDemo.WPF.State.Navigation
{
	/// <summary>
	/// Defines functionality for navigating between views. 
	/// </summary>
	public interface INavigator
	{
		/// <summary>
		/// Holds the current view model.
		/// </summary>
		ViewModelBase CurrentViewModel { get; }

		/// <summary>
		/// Navigates to the view model.
		/// </summary>
		/// <param name="destinationViewModel">The view model to navigate to.</param>
		void GoTo(ViewModelBase destinationViewModel);

		/// <summary>
		/// Navigates to a view model defined by the view type.
		/// </summary>
		/// <param name="destinationViewType">The type of view to navigate to.</param>
		void GoTo(ViewType destinationViewType, object parameter = null);

		/// <summary>
		/// Current view model change event.
		/// </summary>
		event Action CurrentViewModelChanged;
	}
}
