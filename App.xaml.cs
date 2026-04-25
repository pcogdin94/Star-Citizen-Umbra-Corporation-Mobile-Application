namespace UmbraCorpApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		// Keeping light theme regardless of system setting
		Application.Current.UserAppTheme = AppTheme.Light;

		MainPage = new AppShell();
	}
}
