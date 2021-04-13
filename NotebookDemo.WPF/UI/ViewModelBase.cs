using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NotebookDemo.WPF.UI
{
	/// <summary>
	/// Simple <see cref="INotifyPropertyChanged"/> implementation for use in view models.
	/// </summary>
	public class ViewModelBase : INotifyPropertyChanged
	{
		#region Public Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Public Methods

		/// <summary>
		/// Unsubscribe from events and release resources (for transient view models).
		/// </summary>
		public virtual void Dispose() { }

		#endregion

		#region Protected Methods

		/// <summary>
		/// Invokes the <see cref="PropertyChanged"/> for the specified property name.
		/// </summary>
		/// <param name="propertyName">The name of the property.</param>
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
