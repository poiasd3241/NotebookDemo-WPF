using System;
using System.Collections.Generic;
using NotebookDemo.WPF.State.Navigation.ViewModelFactory;

namespace NotebookDemo.WPF.State.Navigation
{
	/// <summary>
	/// Used for navigating between main views of the app: Home,Note.
	/// </summary>
	public class MainNavigator : NavigatorBase
	{
		#region Public Events

		/// <summary>
		/// Navigate to Home view event.
		/// </summary>
		public event Action NavigateHome;

		#endregion

		#region Constructor

		public MainNavigator(IViewModelFactory viewModelFactory) : base(viewModelFactory)
		{ }

		#endregion

		#region Public Methods

		/// <summary>
		/// Invokes the <see cref="NavigateHome"/>.
		/// </summary>
		public void OnNavigateHome()
		{
			NavigateHome?.Invoke();
		}

		#endregion

		#region Protected Members

		protected override List<ViewType> DefineSupportedViews()
		{
			return new()
			{
				ViewType.Home,
				ViewType.Note
			};
		}

		#endregion
	}
}
