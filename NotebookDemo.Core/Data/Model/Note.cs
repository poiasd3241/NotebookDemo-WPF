namespace NotebookDemo.Core.Data.Model
{
	/// <summary>
	/// Domain model, represents a note object.
	/// </summary>
	public class Note
	{
		#region Public Properties

		public int ID { get; set; }
		public string Text { get; set; }
		public bool Important { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Copies the note's properties to a new note object.
		/// </summary>
		/// <returns></returns>
		public Note Copy() => new() { ID = ID, Text = Text, Important = Important };

		public static Note Default() => new() { ID = 0, Text = "", Important = false };

		#endregion
	}
}
