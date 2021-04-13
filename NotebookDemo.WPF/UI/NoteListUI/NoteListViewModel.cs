using System.Collections.Generic;
using System.Linq;
using NotebookDemo.Core.Data.Model;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.State.Navigation;

namespace NotebookDemo.WPF.UI.NoteListUI
{
	public class NoteListViewModel : ViewModelBase
	{
		#region Public Properties

		public List<NoteListItemViewModel> Notes { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Parameterless constructor for XAML designer.
		/// </summary>
		public NoteListViewModel()
		{ }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NoteListViewModel(MainNavigator mainNavigator, IEnumerable<Note> notes, IErrorSpy errorSpy)
		{
			Notes = new(notes.Select(note => new NoteListItemViewModel(errorSpy, mainNavigator, note)));
		}

		#endregion
	}
}
