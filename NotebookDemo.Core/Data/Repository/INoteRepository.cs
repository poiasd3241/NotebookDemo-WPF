using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotebookDemo.Core.Data.Model;

namespace NotebookDemo.Core.Data.Repository
{
	/// <summary>
	/// Defines a centralized place to interact with data sources that manage entities for the domain model <see cref="Note"/>.
	/// </summary>
	public interface INoteRepository
	{
		/// <summary>
		/// Handles the creation and persistence of the <see cref="Note"/> object.
		/// </summary>
		/// <param name="note">The object to create.</param>
		/// <returns>The task that returns the created object.</returns>
		Task<Note> Create(Note note);

		/// <summary>
		/// Handles the updating of the <see cref="Note"/> object.
		/// </summary>
		/// <param name="note">The object to update.</param>
		Task Update(Note note);

		/// <summary>
		/// Handles the deleting of the <see cref="Note"/> object.
		/// </summary>
		/// <param name="note">The object to delete.</param>
		Task Delete(Note note);

		/// <summary>
		/// Gets all <see cref="Note"/> objects.
		/// </summary>
		/// <returns>The task that returns the retrieved objects</returns>
		Task<IEnumerable<Note>> GetAll();

		/// <summary>
		/// Handles the <see cref="Note"/> object creation event.
		/// </summary>
		event EventHandler<Note> CreateNote;

		/// <summary>
		/// Handles the <see cref="Note"/> object update event.
		/// </summary>
		event EventHandler<Note> UpdateNote;

		/// <summary>
		/// Handles the <see cref="Note"/> object deletion event.
		/// </summary>
		event EventHandler<Note> DeleteNote;
	}
}
