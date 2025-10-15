using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Library.TheraHealth.Services;

namespace Maui.TheraHealth.ViewModels;

public class MainViewModel
{

    public ObservableCollection<AppointmentViewModel?> Appointments
    {
        get
        {
            return new ObservableCollection<AppointmentViewModel?>
            (AppointmentServiceProxy
            .Current
            .Appointments
            .Where(
                b => (b?.Patient?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                || (b?.Physician?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
            )
            .Select(b => new AppointmentViewModel(b))
            );
        }
    }
    
    public string? Query { get; set; }
    public void Refresh()
    {
        NotifyPropertyChanged(nameof(Appointments));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
