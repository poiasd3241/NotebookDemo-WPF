using NotebookDemo.WPF.UI;

namespace NotebookDemo.WPF.State.Navigation.ViewModelFactory
{
	/// <summary>
	/// The template for creating view models.
	/// </summary>
	/// <typeparam name="TViewModel">The type of a view model to create.</typeparam>
	/// <returns></returns>
	public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

	/// <summary>
	/// Defines functionality for creating view models.
	/// </summary>
	public interface IViewModelFactory
	{
		/// <summary>
		/// Creates a view model.
		/// </summary>
		/// <param name="viewType">The type of view that the to-be-created view model must represent.</param>
		/// <param name="parameter">The parameter that could be used in creation of a view model.</param>
		/// <returns></returns>
		ViewModelBase CreateViewModel(ViewType viewType, object parameter = null);
	}
}
