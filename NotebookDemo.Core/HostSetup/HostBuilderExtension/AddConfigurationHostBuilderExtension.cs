using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NotebookDemo.Core.HostSetup.HostBuilderExtension
{
	public static class AddConfigurationHostBuilderExtension
	{
		public static IHostBuilder AddConfiguration(this IHostBuilder host)
		{
			host.ConfigureAppConfiguration(c =>
			{
				c.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			});

			return host;
		}
	}
}
