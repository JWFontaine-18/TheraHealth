using Maui.TheraHealth.ViewModels;

namespace Maui.TheraHealth.Views;

public partial class AppointmentsView : ContentPage
{
	public AppointmentsView()
	{
		InitializeComponent();
		BindingContext = new AppointmentViewModel();
	}
}