using UmbraCorpApp.Services;

namespace UmbraCorpApp;

public partial class App : Application
{
	private readonly SupabaseService _supabaseService;
	public App(SupabaseService supabaseService)
	{
		InitializeComponent();

        _supabaseService = supabaseService;


        MainPage = new ContentPage
        {
            Content = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    new ActivityIndicator
                    {
                        IsRunning = true
                    },
                    new Label
                    {
                        Text = "Loading..."
                    }
                }
            }
        };

        CheckLoginStatus();

        // Keeping light theme regardless of system setting
        Application.Current.UserAppTheme = AppTheme.Light;

	}

	private async void CheckLoginStatus()
	{
		await _supabaseService.InitializeAsync();

		bool isLoggedIn = await _supabaseService.TryRestoreSessionAsync();

		if (isLoggedIn)
		{
			MainPage = new AppShell(_supabaseService);
		}
		else
		{
			MainPage = new NavigationPage(new LoginPage(_supabaseService));
		}
	}
}
