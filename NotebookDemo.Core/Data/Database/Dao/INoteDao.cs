using System.Collections.Generic;
using System.Threading.Tasks;
using NotebookDemo.Core.Data.Database.Entity;

namespace NotebookDemo.Core.Data.Database.Dao
{
	/// <summary>
	/// Defines interaction with a database table containing <see cref="NoteDbEntity"/> entities.
	/// </summary>
	public interface INoteDao
	{
		/// <summary>
		/// Creates the entity in a database.
		/// </summary>
		/// <param name="note">The entity to create.</param>
		/// <returns>The task that returns the created entity.</returns>
		Task<NoteDbEntity> Create(NoteDbEntity note);

		/// <summary>
		/// Updates the entity in a database.
		/// </summary>
		/// <param name="note">The entity to update.</param>
		Task Update(NoteDbEntity note);

		/// <summary>
		/// Deletes the entity from a database.
		/// </summary>
		/// <param name="id">The entity to delete.</param>
		Task Delete(int id);

		/// <summary>
		/// Gets all entities from the database.
		/// </summary>
		/// <returns>The task that returns the retrieved entities.</returns>
		Task<IEnumerable<NoteDbEntity>> GetAll();
	}
}
