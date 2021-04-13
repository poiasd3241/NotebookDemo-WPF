using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotebookDemo.WPF.UI;

namespace NotebookDemo.WPF.HostSetup.HostBuilderExtension
{
	public static class AddViewHostBuilderExtension
	{
		public static IHostBuilder AddView(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddSingleton(services => new ShellView(services.GetRequiredService<ShellViewModel>()));
			});

			return host;
		}
	}
}
