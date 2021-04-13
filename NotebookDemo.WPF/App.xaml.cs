using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotebookDemo.WPF.HostSetup;
using NotebookDemo.WPF.State.Exit;
using NotebookDemo.WPF.UI;

namespace NotebookDemo.WPF
{
	public partial class App : Application
	{
		private readonly IHost _host;
		private readonly IExitSpy _exitSpy;

		public App()
		{
			_host = AppHostSetup.BuildAppHost();
			_exitSpy = _host.Services.GetRequiredService<IExitSpy>();
			_exitSpy.ExitRequest += ExitSpy_ExitRequest;
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			Window shell = _host.Services.GetRequiredService<ShellView>();
			shell.Show();
			base.OnStartup(e);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			_exitSpy.OnExit();

			_host.Dispose();

			base.OnExit(e);
		}

		private void ExitSpy_ExitRequest()
		{
			Current.Shutdown();
		}
	}
}
