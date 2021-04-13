using NotebookDemo.Core.Data.Model;

namespace NotebookDemo.Core.Data.Database.Entity
{
	/// <summary>
	/// Database entity for the domain model <see cref="Note"/>.
	/// </summary>
	public class NoteDbEntity
	{
		#region Public Properties

		/// <summary>
		/// Note ID.
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Note content.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Whether the note is important.
		/// </summary>
		public int Important { get; set; }

		#endregion
	}
}
