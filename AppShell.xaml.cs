using System.Windows.Input;
using UmbraCorpApp.Services;

namespace UmbraCorpApp;

public partial class AppShell : Shell
{

	private readonly SupabaseService _supabaseService;

	public AppShell(SupabaseService supabaseService)
	{
		InitializeComponent();

		_supabaseService = supabaseService;

		_ = SetAdminVisibility();


    }

    private async Task SetAdminVisibility()
    {
		try
		{
			bool isAdmin = await _supabaseService.IsAdminAsync();

			AdminFlyoutItem.IsVisible = isAdmin;
		}
		catch (Exception ex)
		{
			await DisplayAlert("Admin Check Error", ex.Message, "OK");
		}
    }

    private async void OnLogoutClicked(Object sender, EventArgs e)
	{
		bool confirm = await DisplayAlert(
			"Logout",
			"Are you sure you want to log out?",
			"Yes",
			"No");

		if (!confirm)
			return;
		await _supabaseService.SignOutAsync();

		Application.Current.MainPage = new NavigationPage(new LoginPage(_supabaseService));
	}
	public ICommand NavToSettingsCommand { get; private set; }
}
