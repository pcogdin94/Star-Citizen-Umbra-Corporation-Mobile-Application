using UmbraCorpApp.Services;

namespace UmbraCorpApp;

public partial class LoginPage : ContentPage
{
	private readonly SupabaseService _supabaseService;

	public LoginPage(SupabaseService supabaseService)
	{
		InitializeComponent();
		_supabaseService = supabaseService;
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		try
		{
			string email = EmailEntry.Text?.Trim() ?? "";
			string password = PasswordEntry.Text?.Trim() ?? "";

			if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
			{
				await DisplayAlert("Missing Info", "Please enter your email and password.", "OK");
				return;
			}

			bool success = await _supabaseService.SignInAsync(email, password);

			if (success)
			{
				//await DisplayAlert("Success", "You are now logged in.", "OK");
				Application.Current.MainPage = new AppShell(_supabaseService);
			}
			else
			{
				await DisplayAlert("Login Failed", "Invalid email or password.", "OK");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}

	private async void OnCreateAccountClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new RegisterPage(_supabaseService));
	}
}