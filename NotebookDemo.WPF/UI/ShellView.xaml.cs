using System.Windows;

namespace NotebookDemo.WPF.UI
{
	public partial class ShellView : Window
	{
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern int GetSystemMetrics(int nIndex);

		public ShellView(ShellViewModel shellViewModel)
		{
			InitializeComponent();
			DataContext = shellViewModel;

			const int CXFRAME = 0x20;
			const int CYFRAME = 0x21;
			const int CXPADDEDBORDER = 0x5C;
			const int CYCAPTION = 0x04;

			int extraWidth = (GetSystemMetrics(CXFRAME) + GetSystemMetrics(CXPADDEDBORDER)) * 2;
			int extraHeight = (GetSystemMetrics(CYFRAME) + GetSystemMetrics(CXPADDEDBORDER)) * 2 + GetSystemMetrics(CYCAPTION);

			var homeView = new HomeUI.HomeView();

			MinWidth = homeView.MinWidth + extraWidth;
			MinHeight = homeView.MinHeight + extraHeight;

			Width = MinWidth;
			Height = 400;
		}
	}
}
