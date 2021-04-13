using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotebookDemo.Core.Data.Database.Dao;
using NotebookDemo.Core.Data.Database.Entity;
using NotebookDemo.Core.Data.Model;

namespace NotebookDemo.Core.Data.Repository
{
	public class NoteRepository : INoteRepository
	{
		#region Private Members

		private readonly INoteDao _noteDao;
		private readonly NoteDbEntityMapper _noteDbEntityMapper;

		#endregion

		#region Public Events

		public event EventHandler<Note> CreateNote;
		public event EventHandler<Note> UpdateNote;
		public event EventHandler<Note> DeleteNote;

		#endregion

		#region Constructor

		public NoteRepository(INoteDao noteDao, NoteDbEntityMapper noteDbEntityMapper)
		{
			_noteDao = noteDao;
			_noteDbEntityMapper = noteDbEntityMapper;
		}

		#endregion

		#region Public Methods

		public async Task<Note> Create(Note note)
		{
			var createdDbEntity = await _noteDao.Create(_noteDbEntityMapper.MapToEntity(note));
			var createdDomainModel = _noteDbEntityMapper.MapFromEntity(createdDbEntity);

			OnCreate(createdDomainModel);
			return createdDomainModel;
		}

		public async Task Update(Note note)
		{
			await _noteDao.Update(_noteDbEntityMapper.MapToEntity(note));
			OnUpdate(note);
		}

		public async Task Delete(Note note)
		{
			await _noteDao.Delete(note.ID);
			OnDelete(note);
		}

		public async Task<IEnumerable<Note>> GetAll()
		{
			var notesFromDb = await _noteDao.GetAll();
			return _noteDbEntityMapper.MapFromEntityList(notesFromDb);
		}

		#endregion

		#region Private Methods

		private void OnCreate(Note creatednote)
		{
			CreateNote?.Invoke(this, creatednote);
		}

		private void OnUpdate(Note updatednote)
		{
			UpdateNote?.Invoke(this, updatednote);
		}

		private void OnDelete(Note deletedNote)
		{
			DeleteNote?.Invoke(this, deletedNote);
		}

		#endregion
	}
}
