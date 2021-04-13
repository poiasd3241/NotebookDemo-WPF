using System;
using System.Collections.Generic;
using System.Linq;
using NotebookDemo.Core.Data.Model;

namespace NotebookDemo.Core.Data.Database.Entity
{
	/// <summary>
	/// Provides mapping between <see cref="NoteDbEntity"/> and <see cref="Note"/> objects.
	/// </summary>
	public class NoteDbEntityMapper : IEntityMapper<NoteDbEntity, Note>
	{
		#region Public Methods

		public Note MapFromEntity(NoteDbEntity entity) => new()
		{
			ID = entity.ID,
			Important = entity.Important switch
			{
				0 => false,
				1 => true,
				_ => throw new Exception($"Unsupported/corrupted database entity {entity}. " +
					$"{entity.Important} must be either 0 or 1.")
			},
			Text = entity.Text
		};

		public NoteDbEntity MapToEntity(Note domainModel) => new()
		{
			ID = domainModel.ID,
			Important = domainModel.Important switch
			{
				false => 0,
				true => 1
			},
			Text = domainModel.Text
		};

		/// <summary>
		/// Maps the list of entities to a list of domain models.
		/// </summary>
		/// <param name="entities">The entities to map from.</param>
		/// <returns>The domain models.</returns>
		public IEnumerable<Note> MapFromEntityList(IEnumerable<NoteDbEntity> entities) =>
			entities.Select(entity => MapFromEntity(entity));

		#endregion
	}
}
