using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Library.TheraHealth.Services;

namespace Maui.TheraHealth.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{

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
    

    public void Refresh()
    {
        NotifyPropertyChanged(nameof(patients));
    }

    public PatientViewModel? selectedPatient { get; set; }
    public string? Query { get; set; }
    public void Delete()
    {
        if(selectedPatient == null)
        {
            return;
        }

        PatientServiceProxy.Current.Delete(selectedPatient?.Model?.Id ?? 0);
        NotifyPropertyChanged(nameof(patients));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
