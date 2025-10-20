using Maui.TheraHealth.ViewModels;

namespace Maui.TheraHealth;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}

	private void AddPatient(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Patient?patientId=0");
	}
	
	private void AddPhysician(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//Physician?physicianId=0");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
        (BindingContext as MainViewModel)?.Refresh();
    }

    private void EditClicked(object sender, EventArgs e)
	{
        var selectedId = (BindingContext as MainViewModel)?.selectedPatient?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//Patient?patientId={selectedId}");
    }

    private void DeleteClicked(object sender, EventArgs e)
	{
		(BindingContext as MainViewModel)?.Delete();
    }



}

