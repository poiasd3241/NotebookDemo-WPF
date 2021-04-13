using Microsoft.Extensions.Hosting;
using NotebookDemo.Core.HostSetup.HostBuilderExtension;

namespace NotebookDemo.Core.HostSetup
{
	/// <summary>
	/// Provides DI for use in other projects.
	/// </summary>
	public class CoreHostSetup
	{
		/// <summary>
		/// Ensures DI for the <see cref="NotebookDemo.Core"/> objects.
		/// </summary>
		/// <returns>The host builder to use in other projects.</returns>
		public static IHostBuilder PrepareCoreHost()
		{
			return Host.CreateDefaultBuilder()
				.AddConfiguration()
				.AddService()
				.AddStore();
		}
	}
}
