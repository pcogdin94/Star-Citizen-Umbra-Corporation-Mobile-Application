using UmbraCorpApp.Services;

namespace UmbraCorpApp;

public partial class RegisterPage : ContentPage
{
	private readonly SupabaseService _supabaseService;
	public RegisterPage(SupabaseService supabaseService)
	{
		InitializeComponent();
		_supabaseService = supabaseService;
	}

	private async void OnRegisterClicked(object sender, EventArgs e)
	{
		try
		{
			string email = EmailEntry.Text?.Trim() ?? "";
            string rsiHandle = RsiHandleEntry.Text?.Trim() ?? "";
            string discord = DiscordEntry.Text?.Trim() ?? "";
            string division = DivisionPicker.SelectedItem?.ToString() ?? "";
            string password = PasswordEntry.Text?.Trim() ?? "";
			string confirmPassword = ConfirmPasswordEntry.Text?.Trim() ?? "";

			if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) || string.IsNullOrWhiteSpace(rsiHandle)
				|| string.IsNullOrWhiteSpace(discord) || string.IsNullOrWhiteSpace(division))
			{
				await DisplayAlert("Missing Info", "Please fill in all boxes before continuing.", "OK");
				return;
			}

			if (password != confirmPassword)
			{
				await DisplayAlert("Password Mismatch", "Passwords do not match.", "OK");
				return;
			}

			bool success = await _supabaseService.SignUpAsync(
				email,
				password,
				rsiHandle,
				discord,
				division);

			if (success)
			{
				await DisplayAlert(
					"Registration Submitted",
					"Your account was created and your membership request is awaiting admin approval.",
					"OK");
			}
			else
			{
				await DisplayAlert(
					"Registration Failed",
					"Your account could not be created.",
					"OK");
			}

		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}

	private async void OnBackToLoginClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}