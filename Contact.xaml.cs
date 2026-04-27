using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using UmbraCorpApp.Services;

namespace UmbraCorpApp;

public partial class Contact : ContentPage
{
	private readonly SupabaseService _supabaseService;
	private string? _userEmail;
    public static readonly HttpClient httpClient = new HttpClient();
	public static readonly HttpClient httpClient2 = new HttpClient();
    private const string DiscordWebhookUrl = "https://discord.com/api/webhooks/1497082676148961391/Gsh02UvFIphn_Goo42iYOPpyYnpz8sYqmKQISjhW2YP_c7_lLTWvIiw-NX06deUDxGFU";
	private const string BBSWebhookUrl = "https://discord.com/api/webhooks/1497277564559954070/-Y4tFr8WRNrN4L2Byw7I7IFFJdHaS76mJd-xo6JNzAXKUQPnyFwvBPIEpJMICEvrQtQP";

    public Contact()
	{
		InitializeComponent();

		_supabaseService = Application.Current.Handler.MauiContext.Services.GetService<SupabaseService>();

		_userEmail = _supabaseService.GetUserEmail();

		if (!string.IsNullOrWhiteSpace(_userEmail))
		{
			UserEmailLabel.Text = $"Submitting as: {_userEmail}";
		}
	}

	async void OnSendClicked(object sender, EventArgs e)
	{

		// Create vars to save text entered by user
		string message = MessageBody.Text?.Trim();
		string purpose = Purpose.SelectedItem?.ToString();
		string theWholeDamnThing = _userEmail + ": " + purpose + ": " + message;

		// Check if there is information entered in text boxes
		if (string.IsNullOrWhiteSpace(message) || string.IsNullOrEmpty(purpose))
		{
			await DisplayAlert("Error", "Please fill in all fields.", "OK");
			return;
		}

		if (string.IsNullOrWhiteSpace(_userEmail))
		{
			await DisplayAlert("Not Logged In", "Could not find your account e-mail, please log in.", "OK");
			return;
		}

			// Set payload for webhook
		var payload = new
		{
			content = theWholeDamnThing
		};

		// Attempt message sending
		try
		{
			HttpResponseMessage response =
				await httpClient.PostAsJsonAsync(DiscordWebhookUrl, payload);
				await httpClient2.PostAsJsonAsync(BBSWebhookUrl, payload);

			if (response.IsSuccessStatusCode)
			{
				await DisplayAlert("Message Sent", "Thank you for your feedback, our team will reach out to you shortly.", "OK");
				MessageBody.Text = string.Empty;
				Purpose.SelectedItem = null;
			}
			else
			{
				string error = await response.Content.ReadAsStringAsync();
				await DisplayAlert("Error", $"Discord returned: {response.StatusCode}\n{error}", "OK");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Could not send message:\n{ex.Message}", "OK");
		}

    }

    private void Purpose_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}