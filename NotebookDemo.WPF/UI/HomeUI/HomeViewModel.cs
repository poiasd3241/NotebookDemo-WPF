using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using NotebookDemo.Core.Data.Model;
using NotebookDemo.Core.Data.Repository;
using NotebookDemo.Core.Data.Store;
using NotebookDemo.WPF.Command;
using NotebookDemo.WPF.Command.Base;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.State.Navigation;
using NotebookDemo.WPF.UI.NoteListUI;

namespace NotebookDemo.WPF.UI.HomeUI
{
	public class HomeViewModel : ViewModelBase
	{
		#region Private Members

		private readonly MainNavigator _mainNavigator;
		private readonly NoteStore _noteStore;
		private readonly IErrorSpy _errorSpy;
		private bool _addNoteHandled;
		private bool _removeNoteHandled;
		private NotifyCollectionChangedEventArgs LastChangeData => _noteStore.LastChangeData;

		#endregion

		#region Public Properties

		public NoteListViewModel ImportantNotes { get; private set; }
		public NoteListViewModel RegularNotes { get; private set; }
		public bool ExistImportant => ImportantNotes.Notes.Any();
		public bool ExistRegular => RegularNotes.Notes.Any();
		public bool ExistAnyNotes => ExistImportant || ExistRegular;
		public ICommand NewNoteCommand { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Parameterless constructor for XAML designer.
		/// </summary>
		public HomeViewModel()
		{ }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public HomeViewModel(INoteRepository noteRepository, MainNavigator mainNavigator, NoteStore noteStore, IErrorSpy errorSpy)
		{
			_mainNavigator = mainNavigator;
			_mainNavigator.NavigateHome += MainNavigator_NavigateHome;

			_noteStore = noteStore;
			_errorSpy = errorSpy;
			_noteStore.PersistCreateNote += NoteStore_PersistCreateNote;
			_noteStore.PersistDeleteNote += NoteStore_PersistDeleteNote;

			new SafeRelayAsyncCommand(errorSpy, async () => await noteStore.Initialize()).Execute();
			ImportantNotes = new(mainNavigator, _noteStore.Notes.Where(note => note.Important), errorSpy);
			RegularNotes = new(mainNavigator, _noteStore.Notes.Where(note => note.Important == false), errorSpy);

			NewNoteCommand = new SafeRelayAsyncCommand(errorSpy, async () =>
			{
				await noteRepository.Create(Note.Default());
				UpdateNoteLists();
				new NavigationCommand(errorSpy, _mainNavigator, ViewType.Note, LastChangeData.NewItems[0]).Execute();
			});
		}

		#endregion

		#region Private Methods

		private void NoteStore_PersistDeleteNote()
		{
			_removeNoteHandled = false;
		}

		private void NoteStore_PersistCreateNote()
		{
			_addNoteHandled = false;
		}

		/// <summary>
		/// Updates the Important and Regular note lists according to changes in NoteStore.
		/// </summary>
		private void UpdateNoteLists()
		{
			if (LastChangeData == null)
			{
				return;
			}
			var action = LastChangeData.Action;

			switch (action)
			{
				case NotifyCollectionChangedAction.Add:
				{
					if (_addNoteHandled)
					{
						return;
					}
					var newNote = (Note)LastChangeData.NewItems[0];
					AddNote(newNote);
					_addNoteHandled = true;
				}
				break;

				case NotifyCollectionChangedAction.Replace:
				{
					var newNote = (Note)LastChangeData.NewItems[0];

					var (isInImportantList, position) = GetNoteListItemInfo(newNote.ID);
					if (newNote.Important != isInImportantList)
					{
						MoveNote(position, newNote);
					}

				}
				break;

				case NotifyCollectionChangedAction.Remove:
				{
					if (_removeNoteHandled)
					{
						return;
					}
					var oldNote = (Note)LastChangeData.OldItems[0];
					var (isInImportantList, position) = GetNoteListItemInfo(oldNote.ID);
					RemoveNote(position, isInImportantList);
					_removeNoteHandled = true;
				}
				break;
			}
		}

		private void MainNavigator_NavigateHome()
		{
			UpdateNoteLists();
		}

		private (bool isInImportantList, int position) GetNoteListItemInfo(int id)
		{
			int position;
			Predicate<NoteListItemViewModel> matchByID = (noteListItem) => noteListItem.ID == id;

			bool isInImportantList = ImportantNotes.Notes.FirstOrDefault(matchByID.Invoke) != null;
			if (isInImportantList)
			{
				position = ImportantNotes.Notes.FindIndex(matchByID);
			}
			else
			{
				position = RegularNotes.Notes.FindIndex(matchByID);
			}
			return (isInImportantList, position);
		}

		private void MoveNote(int currentNotePosition, Note newNote)
		{
			RemoveNote(currentNotePosition, newNote.Important == false);
			AddNote(newNote);
		}

		private void AddNote(Note newNote)
		{
			if (newNote.Important)
			{
				AddImportant(newNote);
			}
			else
			{
				AddRegular(newNote);
			}
		}

		private void RemoveNote(int currentNotePosition, bool isInImportantList)
		{
			if (isInImportantList)
			{
				RemoveImportantAt(currentNotePosition);
			}
			else
			{
				RemoveRegularAt(currentNotePosition);
			}
		}

		private void AddImportant(Note note)
		{
			ImportantNotes.Notes.Add(new(_errorSpy, _mainNavigator, note));
		}

		private void AddRegular(Note note)
		{
			RegularNotes.Notes.Add(new(_errorSpy, _mainNavigator, note));
		}

		private void RemoveImportantAt(int position)
		{
			ImportantNotes.Notes.RemoveAt(position);
		}
		private void RemoveRegularAt(int position)
		{
			RegularNotes.Notes.RemoveAt(position);
		}

		#endregion
	}
}
