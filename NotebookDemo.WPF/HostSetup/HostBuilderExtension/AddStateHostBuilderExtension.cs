using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.State.Navigation;

namespace NotebookDemo.WPF.HostSetup.HostBuilderExtension
{
	public static class AddStateHostBuilderExtension
	{
		public static IHostBuilder AddState(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddSingleton<MainNavigator>();
				services.AddSingleton<IExitSpy, ExitSpy>();
				services.AddSingleton<IErrorSpy, ErrorSpy>();
			});

			return host;
		}
	}
}
