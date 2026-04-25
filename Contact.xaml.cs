using System.Net.Http.Json;

namespace UmbraCorpApp;

public partial class Contact : ContentPage
{
    public static readonly HttpClient httpClient = new HttpClient();
	public static readonly HttpClient httpClient2 = new HttpClient();
    private const string DiscordWebhookUrl = "https://discord.com/api/webhooks/1497082676148961391/Gsh02UvFIphn_Goo42iYOPpyYnpz8sYqmKQISjhW2YP_c7_lLTWvIiw-NX06deUDxGFU";
	private const string BBSWebhookUrl = "https://discord.com/api/webhooks/1497277564559954070/-Y4tFr8WRNrN4L2Byw7I7IFFJdHaS76mJd-xo6JNzAXKUQPnyFwvBPIEpJMICEvrQtQP";

    public Contact()
	{
		InitializeComponent();
	}

	async void OnSendClicked(object sender, EventArgs e)
	{

		// Create vars to save text entered by user
		string email = YourEmail.Text?.Trim();
		string message = MessageBody.Text?.Trim();
		string theWholeDamnThing = email + ": " + message;

		// Check if there is information entered in text boxes
		if (string.IsNullOrWhiteSpace(email))
		{
			await DisplayAlert("Error", "Please enter an e-mail.", "OK");
			return;
		}
		else if (string.IsNullOrWhiteSpace(message))
		{
			await DisplayAlert("Error", "Please enter a message.", "OK");
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