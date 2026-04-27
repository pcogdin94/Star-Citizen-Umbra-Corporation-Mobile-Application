using UmbraCorpApp.Services;

namespace UmbraCorpApp;

public partial class MainPage : ContentPage
{
	private readonly SupabaseService _supabaseService;
	public MainPage(SupabaseService supabaseService)
	{
		InitializeComponent();

		_supabaseService = supabaseService;

		LoadHomeVideo();
	}

	private async void LoadHomeVideo()
	{
		try
		{
			string? videoUrl = await _supabaseService.GetHomeVideoUrlAsync();

			if (string.IsNullOrWhiteSpace(videoUrl))
			{
				await DisplayAlert("Video Error", "No home video URL was found.", "OK");
				return;
			}

			HomeVideoWebView.Source = videoUrl;
		}
		catch (Exception ex)
		{
			await DisplayAlert("Video Error", ex.Message, "OK");
		}
	}
}

