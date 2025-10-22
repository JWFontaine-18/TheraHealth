using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Library.TheraHealth.Models;
using Library.TheraHealth.Services;

namespace Maui.TheraHealth.ViewModels;

public class PhysicianViewModel
{

    public PhysicianViewModel()
    {
        Model = new Physician();
        SetUpCommands();
    }

    public PhysicianViewModel(Physician? model)
    {
        Model = model;
        SetUpCommands();
    }
    private void SetUpCommands()
    {
        DeleteCommand = new Command(DoDelete);
        EditCommand = new Command((p) => DoEdit(p as PhysicianViewModel));
    }

    private void DoDelete()
    {
        if (Model?.Id > 0)
        {
            PhysicianServiceProxy.Current.Delete(Model.Id);
            Shell.Current.GoToAsync("//MainPage");
        }
    }

    private void DoEdit(PhysicianViewModel? bv)
    {
        if (bv == null)
        {
            return;
        }
        var selectedId = bv?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//Physician?physicianId={selectedId}");
    }
    
    public Physician? Model { get; set; }
    
    public ICommand? DeleteCommand { get; set; }
    public ICommand? EditCommand { get; set; }




}
