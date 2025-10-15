using Maui.TheraHealth.ViewModels;

namespace Maui.TheraHealth;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}

    private void AddClicked(object sender, EventArgs e)
	{
         Shell.Current.GoToAsync("//Appointments");
    }

}

