using Maui.TheraHealth.ViewModels;

namespace Maui.TheraHealth.Views;

public partial class PatientsView : ContentPage
{
	public PatientsView()
	{
		InitializeComponent();
		BindingContext = new PatientViewModel();
	}
}