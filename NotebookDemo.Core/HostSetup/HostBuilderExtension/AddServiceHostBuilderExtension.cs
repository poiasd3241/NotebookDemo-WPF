using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotebookDemo.Core.Data.Database.Dao;
using NotebookDemo.Core.Data.Database.Entity;
using NotebookDemo.Core.Data.Repository;

namespace NotebookDemo.Core.HostSetup.HostBuilderExtension
{
	public static class AddServiceHostBuilderExtension
	{
		public static IHostBuilder AddService(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddSingleton<INoteDao, NoteDao>();
				services.AddSingleton<INoteRepository, NoteRepository>();
				services.AddSingleton<NoteDbEntityMapper>();
			});

			return host;
		}
	}
}
