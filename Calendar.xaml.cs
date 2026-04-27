namespace UmbraCorpApp;

public partial class Calendar : ContentPage
{
	public Calendar()
	{
		InitializeComponent();

		ucCalendar.Source = "https://umbra-site-five.vercel.app/calendar-embed?key=umbra-calendar-2026-private-key";


    }
}