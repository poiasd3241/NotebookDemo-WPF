using System;
using NotebookDemo.Core.Data.Model;
using NotebookDemo.WPF.UI;
using NotebookDemo.WPF.UI.HomeUI;
using NotebookDemo.WPF.UI.NoteUI;

namespace NotebookDemo.WPF.State.Navigation.ViewModelFactory
{
	/// <summary>
	/// The template for creating <see cref="NoteViewModel"/> view models.
	/// </summary>
	/// <param name="note">The note object that the to-be-created view model must represent.</param>
	/// <returns></returns>
	public delegate NoteViewModel CreateNoteViewModel(Note note);

	/// <summary>
	/// Default implementation of <see cref="IViewModelFactory"/>.
	/// </summary>
	public class DefaultViewModelFactory : IViewModelFactory
	{
		#region Private Members

		private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
		private readonly CreateNoteViewModel _createNoteViewModel;

		#endregion

		#region Constructor

		public DefaultViewModelFactory(
			CreateViewModel<HomeViewModel> createHomeViewModel, CreateNoteViewModel createNoteViewModel)
		{
			_createHomeViewModel = createHomeViewModel;
			_createNoteViewModel = createNoteViewModel;
		}

		#endregion

		#region Public Methods

		public ViewModelBase CreateViewModel(ViewType viewType, object parameter)
		{
			return viewType switch
			{
				ViewType.Home => _createHomeViewModel(),
				ViewType.Note => _createNoteViewModel((Note)parameter),
				_ => throw new InvalidOperationException($"{viewType} doesn't have a ViewModel."),
			};
		}

		#endregion
	}
}
