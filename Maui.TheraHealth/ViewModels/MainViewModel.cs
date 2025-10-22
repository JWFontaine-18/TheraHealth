using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Library.TheraHealth.Services;

namespace Maui.TheraHealth.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{

    public ObservableCollection<PhysicianViewModel?> physicians
    {
        get
        {
            return new ObservableCollection<PhysicianViewModel?>
            (PhysicianServiceProxy
            .Current
            .Physicians
            .Where(
                b => b?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false
            )
            .Select(b => new PhysicianViewModel(b))
            );
        }
    }

    public ObservableCollection<PatientViewModel?> patients
    {
        get
        {
            return new ObservableCollection<PatientViewModel?>
            (PatientServiceProxy
            .Current
            .Patients
            .Where(
                b => b?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false
            )
            .Select(b => new PatientViewModel(b))
            );
        }
    }

    public ObservableCollection<AppointmentViewModel?> appointments
    {
        get
        {
            return new ObservableCollection<AppointmentViewModel?>
            (AppointmentServiceProxy
            .Current
            .Appointment
            .Where(a => a != null)
            .Select(a => new AppointmentViewModel(a))
            );
        }
    }


    public void Refresh()
    {
        NotifyPropertyChanged(nameof(patients));
        NotifyPropertyChanged(nameof(physicians));
        NotifyPropertyChanged(nameof(appointments));
    }

    public PatientViewModel? selectedPatient { get; set; }
    public PhysicianViewModel? selectedPhysician { get; set; }
    public AppointmentViewModel? selectedAppointment { get; set; }
    public string? Query { get; set; }
    public void DeletePatient()
    {
        if (selectedPatient == null)
        {
            return;
        }

        PatientServiceProxy.Current.Delete(selectedPatient?.Model?.Id ?? 0);
        NotifyPropertyChanged(nameof(patients));
    }

    public void DeletePhysician()
    {
        if(selectedPhysician == null)
        {
            return;
        }

        PhysicianServiceProxy.Current.Delete(selectedPhysician?.Model?.Id ?? 0);
        NotifyPropertyChanged(nameof(physicians));
    }

    public void DeleteAppointment()
    {
        if(selectedAppointment == null)
        {
            return;
        }

        AppointmentServiceProxy.Current.Delete(selectedAppointment?.Model?.Id ?? 0);
        NotifyPropertyChanged(nameof(appointments));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
