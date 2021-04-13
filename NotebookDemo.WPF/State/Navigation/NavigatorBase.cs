using System;
using System.Collections.Generic;
using NotebookDemo.WPF.State.Navigation.ViewModelFactory;
using NotebookDemo.WPF.UI;

namespace NotebookDemo.WPF.State.Navigation
{
	/// <summary>
	/// Defines base navigator functionality.
	/// </summary>
	public abstract class NavigatorBase : INavigator
	{
		#region Private Members

		private readonly IViewModelFactory _viewModelFactory;
		private readonly List<ViewType> _supportedViews;

		#endregion

		#region Abstract Members

		protected abstract List<ViewType> DefineSupportedViews();

		#endregion

		#region Public Properties

		private ViewModelBase _currentViewModel;
		public ViewModelBase CurrentViewModel
		{
			get => _currentViewModel;
			set
			{
				if (_currentViewModel != value)
				{
					_currentViewModel = value;
					OnCurrentViewModelChanged();
				}
			}
		}

		#endregion

		#region Public Events

		public event Action CurrentViewModelChanged;

		#endregion

		#region Constructor

		public NavigatorBase(IViewModelFactory viewModelFactory)
		{
			_viewModelFactory = viewModelFactory;
			_supportedViews = DefineSupportedViews();
		}

		#endregion

		#region Public Methods

		public void GoTo(ViewModelBase destinationViewModel)
		{
			CurrentViewModel = destinationViewModel;
		}

		public void GoTo(ViewType destinationViewType, object parameter = null)
		{
			if (_supportedViews != null && _supportedViews.Count != 0)
			{
				if (_supportedViews.Contains(destinationViewType))
				{
					GoTo(_viewModelFactory.CreateViewModel(destinationViewType, parameter));
					return;
				}

				throw new InvalidOperationException($"{destinationViewType} isn't supported.");
			}

			throw new InvalidOperationException($"Supported {nameof(ViewType)} not defined in {nameof(_supportedViews)}.");
		}

		#endregion

		#region Private Methods

		private void OnCurrentViewModelChanged()
		{
			CurrentViewModelChanged?.Invoke();
		}

		#endregion
	}
}
