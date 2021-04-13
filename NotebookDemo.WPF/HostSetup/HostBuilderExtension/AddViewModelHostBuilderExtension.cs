using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotebookDemo.Core.Data.Model;
using NotebookDemo.Core.Data.Repository;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.State.Navigation;
using NotebookDemo.WPF.State.Navigation.ViewModelFactory;
using NotebookDemo.WPF.UI;
using NotebookDemo.WPF.UI.ErrorUI;
using NotebookDemo.WPF.UI.HomeUI;
using NotebookDemo.WPF.UI.NoteUI;

namespace NotebookDemo.WPF.HostSetup.HostBuilderExtension
{
	public static class AddViewModelHostBuilderExtension
	{
		public static IHostBuilder AddViewModel(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddSingleton<ShellViewModel>();
				services.AddSingleton<HomeViewModel>();
				services.AddTransient<ErrorViewModel>();

				services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
				services.AddTransient<CreateNoteViewModel>(services => (Note note) => CreateNoteViewModel(services, note));

				services.AddSingleton<IViewModelFactory, DefaultViewModelFactory>();

			});

			return host;
		}

		private static NoteViewModel CreateNoteViewModel(IServiceProvider services, Note note) =>
				 new(note,
					services.GetRequiredService<MainNavigator>(),
					services.GetRequiredService<INoteRepository>(),
					services.GetRequiredService<IExitSpy>(),
					services.GetRequiredService<IErrorSpy>());
	}
}
