using System.Windows.Input;
using NotebookDemo.Core.Data.Model;
using NotebookDemo.Core.Data.Repository;
using NotebookDemo.WPF.Command;
using NotebookDemo.WPF.Command.Base;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.State.Navigation;

namespace NotebookDemo.WPF.UI.NoteUI
{
	public class NoteViewModel : ViewModelBase
	{
		#region Private Members

		private readonly Note _note;
		private readonly MainNavigator _mainNavigator;
		private readonly IExitSpy _exitSpy;
		private readonly UpdateNoteCommand _updateNoteCommand;

		#endregion

		#region Public Properties
		public Note Note => _note;

		public bool Important
		{
			get => _note.Important;
			set
			{
				if (_note.Important != value)
				{
					_note.Important = value;
				}

				if (_updateNoteCommand?.IsExecuting == false)
				{
					_updateNoteCommand.Execute(null);
				}
			}
		}
		public string Text
		{
			get => _note.Text;
			set
			{
				if (_note.Text != value)
				{
					_note.Text = value;
				}

				if (_updateNoteCommand?.IsExecuting == false)
				{
					_updateNoteCommand.Execute(null);
				}
			}
		}
		public string Title => $"Note #{_note.ID}";

		public ICommand DeleteNoteCommand { get; }
		public ICommand BackToNoteListCommand { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Parameterless constructor for XAML designer.
		/// </summary>
		public NoteViewModel()
		{ }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NoteViewModel(Note note, MainNavigator mainNavigator, INoteRepository noteRepository, IExitSpy exitSpy, IErrorSpy errorSpy)
		{
			_note = note;
			_mainNavigator = mainNavigator;
			_exitSpy = exitSpy;
			_exitSpy.Exit += ExitSpy_Exit;

			// Navigate home command.
			var navigateHomeCommand = new SafeRelayCommand(errorSpy, () =>
			{
				_mainNavigator.OnNavigateHome();
				new NavigationCommand(errorSpy, _mainNavigator, ViewType.Home).Execute();
				Dispose();
			});

			// Update note command.
			_updateNoteCommand = new(errorSpy, noteRepository, this);

			// Back to note list command.
			BackToNoteListCommand = new SafeRelayCommand(errorSpy, () =>
			{
				SkipDelayThenUpdateImmediately();
				navigateHomeCommand.Execute();
			});

			// Delete note command.
			DeleteNoteCommand = new SafeRelayAsyncCommand(errorSpy, async () =>
			{
				CancelUpdate();
				await noteRepository.Delete(_note);
				navigateHomeCommand.Execute();
			});
		}

		#endregion

		#region Public Methods

		public override void Dispose()
		{
			_exitSpy.Exit -= ExitSpy_Exit;
		}

		#endregion


		#region Private Methods

		private void SkipDelayThenUpdateImmediately()
		{
			if (_updateNoteCommand.Cancel())
			{
				_updateNoteCommand.UpdateWithDelay = false;
				_updateNoteCommand.Execute();
			}
		}

		private void CancelUpdate()
		{
			_updateNoteCommand.Cancel();
		}

		private void ExitSpy_Exit()
		{
			SkipDelayThenUpdateImmediately();
			Dispose();
		}

		#endregion
	}
}
