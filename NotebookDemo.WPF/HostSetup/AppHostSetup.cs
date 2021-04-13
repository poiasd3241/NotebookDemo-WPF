using Microsoft.Extensions.Hosting;
using NotebookDemo.Core.HostSetup;
using NotebookDemo.WPF.HostSetup.HostBuilderExtension;

namespace NotebookDemo.WPF.HostSetup
{
	/// <summary>
	/// Provides the DI for the app.
	/// </summary>
	public class AppHostSetup
	{
		/// <summary>
		/// Ensures DI for the <see cref="NotebookDemo.WPF"/> objects.<br/>
		/// </summary>
		/// <returns>The built host.</returns>
		public static IHost BuildAppHost()
		{
			return CoreHostSetup.PrepareCoreHost()
				.AddState()
				.AddViewModel()
				.AddView()
				.Build();
		}
	}
}
