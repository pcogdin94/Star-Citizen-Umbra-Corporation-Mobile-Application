using System.Windows.Input;

namespace UmbraCorpApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

//		NavToSettingsCommand = new Command(async () => {
//			await DisplayAlert("Menu Item", "Settings Selected", "OK");
//			this.FlyoutIsPresented = this.FlyoutBehavior != FlyoutBehavior.Flyout;
//		});

//        BindingContext = this;
	}

	public ICommand NavToSettingsCommand { get; private set; }
}
