using System;
using System.Threading;
using System.Threading.Tasks;
using NotebookDemo.Core.Data.Repository;
using NotebookDemo.WPF.Command.Base;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.UI.NoteUI;

namespace NotebookDemo.WPF.Command
{
	/// <summary>
	/// Updates the note with a specified delay. Cancellable.
	/// </summary>
	public class UpdateNoteCommand : AsyncCommandBase
	{
		#region Private Members

		private readonly INoteRepository _noteRepository;
		private readonly NoteViewModel _noteViewModel;
		private readonly CancellationTokenSource _cancellation;
		private CancellationToken CancelToken => _cancellation.Token;
		private readonly int _updateDelay;

		#endregion

		#region Public Properties

		public bool UpdateWithDelay { get; set; } = false;
		public bool IsDelayStage { get; private set; }

		#endregion

		#region Constructor

		public UpdateNoteCommand(IErrorSpy errorSpy, INoteRepository noteRepository, NoteViewModel noteViewModel) : base(errorSpy)
		{
			_noteRepository = noteRepository;
			_noteViewModel = noteViewModel;
			_cancellation = new();
			_updateDelay = 4000;
		}

		#endregion

		#region Public Methods

		public bool Cancel()
		{
			if (IsDelayStage)
			{
				_cancellation.Cancel();
				return true;
			}
			return false;
		}

		#endregion

		#region Protected Members

		protected async override Task ExecuteAsync()
		{
			try
			{
				IsDelayStage = true;
				if (UpdateWithDelay)
				{
					await Task.Delay(_updateDelay, CancelToken);
				}
				await _noteRepository.Update(_noteViewModel.Note);
			}
			catch (OperationCanceledException)
			{
				if (CancelToken.IsCancellationRequested)
				{
					return;
				}
				else
				{
					throw;
				}
			}
			finally
			{
				IsDelayStage = false;
			}
		}

		#endregion
	}
}
