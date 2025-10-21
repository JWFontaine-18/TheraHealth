using Library.TheraHealth.Models;
using Maui.TheraHealth.ViewModels;

namespace Maui.TheraHealth.Views;

[QueryProperty(nameof(appointmentId), "appointmentId")]
public partial class AppointmentsView : ContentPage
{
	public int appointmentId { get; set; }
	public AppointmentsView()
	{
		InitializeComponent();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		if (appointmentId == 0)
		{
			BindingContext = new Appointment();
		}
		else
		{
			BindingContext = new Appointment(appointmentId);
		}
	}
}