using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
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
		// Clear and reload patients and physicians for pickers
		PatientPicker.ItemsSource = null;
		PhysicianPicker.ItemsSource = null;
		PatientPicker.ItemsSource = PatientServiceProxy.Current.Patients;
		PhysicianPicker.ItemsSource = PhysicianServiceProxy.Current.Physicians;

		if (appointmentId == 0)
		{
			BindingContext = new Appointment();
			AppointmentDate.Date = DateTime.Today;
			AppointmentTime.Time = new TimeSpan(8, 0, 0);
			PatientPicker.SelectedItem = null;
			PhysicianPicker.SelectedItem = null;
		}
		else
		{
			var appointment = new Appointment(appointmentId);
			BindingContext = appointment;

			// Set selected items based on appointment
			if (appointment.PatientId > 0)
			{
				PatientPicker.SelectedItem = PatientServiceProxy.Current.Patients.FirstOrDefault(p => (p?.Id ?? 0) == appointment.PatientId);
			}
			if (appointment.PhysicianId > 0)
			{
				PhysicianPicker.SelectedItem = PhysicianServiceProxy.Current.Physicians.FirstOrDefault(ph => (ph?.Id ?? 0) == appointment.PhysicianId);
			}
			if (appointment.DateTime != null)
			{
				AppointmentDate.Date = appointment.DateTime.Value.Date;
				AppointmentTime.Time = appointment.DateTime.Value.TimeOfDay;
			}
		}
	}

	private void OkClicked(object sender, EventArgs e)
	{
		var appointment = BindingContext as Appointment;
		if (appointment == null)
		{
			return;
		}

		// Get selected patient and physician
		var selectedPatient = PatientPicker.SelectedItem as Patient;
		var selectedPhysician = PhysicianPicker.SelectedItem as Physician;

		// Validate patient and physician selection
		if (selectedPatient == null)
		{
			DisplayAlert("Error", "Please select a patient.", "OK");
			return;
		}

		if (selectedPhysician == null)
		{
			DisplayAlert("Error", "Please select a physician.", "OK");
			return;
		}

		// Set patient and physician IDs
		appointment.PatientId = selectedPatient.Id;
		appointment.PhysicianId = selectedPhysician.Id;
		appointment.Patient = selectedPatient;
		appointment.Physician = selectedPhysician;

		// Combine date and time
		var combinedDateTime = AppointmentDate.Date + AppointmentTime.Time;
		appointment.DateTime = combinedDateTime;

		try
		{
			AppointmentServiceProxy.Current.AddOrUpdate(appointment);
			Shell.Current.GoToAsync("//MainPage");
		}
		catch (Exception ex)
		{
			DisplayAlert("Error", ex.Message, "OK");
		}
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}
}