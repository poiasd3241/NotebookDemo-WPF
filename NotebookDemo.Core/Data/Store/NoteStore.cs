using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using NotebookDemo.Core.Data.Model;
using NotebookDemo.Core.Data.Repository;

namespace NotebookDemo.Core.Data.Store
{
	/// <summary>
	/// Persists the <see cref="Note"/> objects in memory during app runtime.
	/// </summary>
	public class NoteStore
	{
		#region Private Members

		private readonly INoteRepository _noteRepository;

		#endregion

		#region Public Properties

		/// <summary>
		/// All note objects, kept in sync with a repository.
		/// </summary>
		public ObservableCollection<Note> Notes { get; private set; }

		/// <summary>
		/// Contains last <see cref="Notes"/> change data.
		/// </summary>
		public NotifyCollectionChangedEventArgs LastChangeData { get; private set; }

		public bool Initialized { get; private set; } = false;

		#endregion

		#region Public Events

		public event NotifyCollectionChangedEventHandler NotesChanged;

		public event Action PersistCreateNote;
		public event Action PersistDeleteNote;
		public event Action PersistUpdateNote;

		#endregion

		#region Constructor

		public NoteStore(INoteRepository noteRepository)
		{
			_noteRepository = noteRepository;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Initializes the store with the up-to-date <see cref="Note"/> objects from a repository.
		/// </summary>
		public async Task Initialize()
		{
			Notes = new(await _noteRepository.GetAll());
			Notes.CollectionChanged += OnNotesChanged;

			_noteRepository.CreateNote += NoteRepository_CreateNote;
			_noteRepository.UpdateNote += NoteRepository_UpdateNote;
			_noteRepository.DeleteNote += NoteRepository_DeleteNote;

			Initialized = true;
		}

		#endregion

		#region Private Methods

		private void OnNotesChanged(object sender, NotifyCollectionChangedEventArgs lastChangeData)
		{
			LastChangeData = lastChangeData;
			NotesChanged?.Invoke(sender, LastChangeData);
		}

		private void NoteRepository_CreateNote(object sender, Note createdNote)
		{
			Notes.Add(createdNote);
			PersistCreateNote?.Invoke();
		}

		private void NoteRepository_UpdateNote(object sender, Note updatedNote)
		{
			var toUpdate = Notes.First(note => note.ID == updatedNote.ID);
			var position = Notes.IndexOf(toUpdate);

			Notes[position] = updatedNote;
			PersistUpdateNote?.Invoke();
		}

		private void NoteRepository_DeleteNote(object sender, Note deletedNote)
		{
			Notes.Remove(deletedNote);
			PersistDeleteNote?.Invoke();
		}

		#endregion
	}
}
