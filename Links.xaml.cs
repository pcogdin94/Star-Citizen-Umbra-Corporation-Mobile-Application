namespace UmbraCorpApp;

public partial class Links : ContentPage
{
    public Links()
    {
        InitializeComponent();
    }

    // UCOR Site
    private async void OnUcorSiteClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://umbra-site-five.vercel.app/");
    }

    // Discord
    private async void OnDiscordClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.discord.gg/dTxh6rauzM");
    }

    // RSI
    private async void OnRSIClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.robertsspaceindustries.com/en/orgs/UCOR");
    }

    // Spectrum
    private async void OnSpectrumClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.robertsspaceindustries.com/spectrum/community/UCOR");
    }

    // Starjump Fleet Viewer
    private async void OnSjfvSiteClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://hangar.link/");
    }

    // Erkul
    private async void OnErkulClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.erkul.games/live/ships");
    }

    // SP Viewer
    private async void OnSPViewerClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.spviewer.eu/");
    }

}