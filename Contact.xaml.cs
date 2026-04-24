using System.Net.Http.Json;

namespace UmbraCorpApp;

public partial class Contact : ContentPage
{
    public static readonly HttpClient httpClient = new HttpClient();
	public static readonly HttpClient httpClient2 = new HttpClient();
    private const string DiscordWebhookUrl = "";
	private const string BBSWebhookUrl = "";

    public Contact()
	{
		InitializeComponent();
	}

	async void OnSendClicked(object sender, EventArgs e)
	{
		string email = YourEmail.Text?.Trim();
		string message = MessageBody.Text?.Trim();
		string theWholeDamnThing = email + ": " + message;

        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Please enter an e-mail.", "OK");
        }

        if (string.IsNullOrWhiteSpace(message))
		{
			await DisplayAlert("Error", "Please enter a message.", "OK");
			return;
		}
	
		var payload = new
		{
			content = theWholeDamnThing
		};

		try
		{
			HttpResponseMessage response =
				await httpClient.PostAsJsonAsync(DiscordWebhookUrl, payload);
				await httpClient2.PostAsJsonAsync(BBSWebhookUrl, payload);

			if (response.IsSuccessStatusCode)
			{
				await DisplayAlert("Message Sent", "Thank you for your feedback!", "OK");
				MessageBody.Text = string.Empty;
				YourEmail.Text = string.Empty;
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
}
