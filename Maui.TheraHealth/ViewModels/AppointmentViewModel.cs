using System;
using System.Windows.Input;
using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
using Maui.TheraHealth.Views;

namespace Maui.TheraHealth.ViewModels;

public class AppointmentViewModel
{
    public AppointmentViewModel()
    {
        Model = new Appointment();
        SetUpCommands();
    }

    public AppointmentViewModel(Appointment? model)
    {
        Model = model;
        SetUpCommands();
    }
    private void SetUpCommands()
    {
        DeleteCommand = new Command(DoDelete);
        EditCommand = new Command((p) => DoEdit(p as AppointmentViewModel));
    }

    private void DoDelete()
    {
        if (Model?.Id > 0)
        {
            AppointmentServiceProxy.Current.Delete(Model.Id);
            Shell.Current.GoToAsync("//MainPage");
        }
    }

    private void DoEdit(AppointmentViewModel? bv)
    {
        if (bv == null)
        {
            return;
        }
        var selectedId = bv?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//Blog?blogId={selectedId}");
    }
    
    public Appointment? Model { get; set; }
    
    public ICommand? DeleteCommand { get; set; }
    public ICommand? EditCommand { get; set; }
}
