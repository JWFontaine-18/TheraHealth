using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
using Maui.TheraHealth.ViewModels;

namespace Maui.TheraHealth.Views;


[QueryProperty(nameof(patientId), "patientId")]
public partial class PatientsView : ContentPage
{
	public int patientId { get; set; }
	public PatientsView()
	{
		InitializeComponent();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		if (patientId == 0)
		{
			BindingContext = new Patient();
		}
		else
		{
			BindingContext = new Patient(patientId);
		}
	}

	private void OkClicked(object sender, EventArgs e)
	{
		PatientServiceProxy.Current.AddOrUpdate(BindingContext as Patient);

		Shell.Current.GoToAsync("//MainPage");
	}

    private void CancelClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//MainPage");
    }


}