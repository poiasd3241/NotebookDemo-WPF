using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotebookDemo.Core.Data.Store;

namespace NotebookDemo.Core.HostSetup.HostBuilderExtension
{
	public static class AddStoreHostBuilderExtension
	{
		public static IHostBuilder AddStore(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddSingleton<NoteStore>();
			});

			return host;
		}
	}
}
