using System;
using System.Windows.Input;
using Library.TheraHealth.Models;
using Library.TheraHealth.Services;

namespace Maui.TheraHealth.ViewModels;

public class PatientViewModel
{
    public PatientViewModel()
    {
        Model = new Patient();
        SetUpCommands();
    }

    public PatientViewModel(Patient? model)
    {
        Model = model;
        SetUpCommands();
    }
    private void SetUpCommands()
    {
        DeleteCommand = new Command(DoDelete);
        EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
    }

    private void DoDelete()
    {
        if (Model?.Id > 0)
        {
            PatientServiceProxy.Current.Delete(Model.Id);
            Shell.Current.GoToAsync("//MainPage");
        }
    }

    private void DoEdit(PatientViewModel? bv)
    {
        if (bv == null)
        {
            return;
        }
        var selectedId = bv?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//Blog?blogId={selectedId}");
    }
    
    public Patient? Model { get; set; }
    
    public ICommand? DeleteCommand { get; set; }
    public ICommand? EditCommand { get; set; }
}
