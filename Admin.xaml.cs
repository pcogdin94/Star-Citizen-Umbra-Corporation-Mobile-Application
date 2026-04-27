using UmbraCorpApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace UmbraCorpApp;


public partial class Admin : ContentPage
{

	private readonly SupabaseService _supabaseService;

	public Admin()
	{

		InitializeComponent();

		adminConsole.Source = "https://umbra-site-five.vercel.app/control-room";

    }
}