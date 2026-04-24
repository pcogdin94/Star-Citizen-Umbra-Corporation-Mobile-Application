namespace UmbraCorpApp;

public partial class Links : ContentPage
{
    public Links()
    {
        InitializeComponent();
    }

    private async void OnDiscordClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.discord.gg/dTxh6rauzM");
    }

    private async void OnRSIClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.robertsspaceindustries.com/en/orgs/UCOR");
    }

    private async void OnSpectrumClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.robertsspaceindustries.com/spectrum/community/UCOR");
    }

    private async void OnErkulClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.erkul.games/live/ships");
    }

    private async void OnSPViewerClicked(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://www.spviewer.eu/");
    }

}