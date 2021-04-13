using System.Windows.Input;
using NotebookDemo.Core.Data.Model;
using NotebookDemo.WPF.Command;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.State.Navigation;

namespace NotebookDemo.WPF.UI.NoteListUI
{
	public class NoteListItemViewModel : ViewModelBase
	{
		#region Public Properties

		public Note Note { get; private set; }
		public int ID => Note.ID;
		public bool Important => Note.Important;
		public string Text => Note.Text;
		public bool IsEmptyNote => string.IsNullOrWhiteSpace(Text);
		public ICommand OpenNoteCommand { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Parameterless constructor for XAML designer.
		/// </summary>
		public NoteListItemViewModel()
		{ }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NoteListItemViewModel(IErrorSpy errorSpy, MainNavigator mainNavigator, Note note)
		{
			Note = note;
			OpenNoteCommand = new NavigationCommand(errorSpy, mainNavigator, ViewType.Note, Note);
		}

		#endregion
	}
}
