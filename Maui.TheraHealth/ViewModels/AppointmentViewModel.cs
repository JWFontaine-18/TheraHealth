using System;
using Library.TheraHealth.Models;
using Library.TheraHealth.Services;
using Maui.TheraHealth.Views;

namespace Maui.TheraHealth.ViewModels;

public class AppointmentViewModel
{
    private PatientServiceProxy _patientSvc;
    private PhysicianServiceProxy _physicianSvc;
    private AppointmentServiceProxy _appointmentSvc;

    private List<Appointment> appointments;
    public List<Appointment> Appointments
    {
        get
        {
            return appointments;
        }
    }

    public AppointmentViewModel()
    {
        _patientSvc = PatientServiceProxy.Current;
        _physicianSvc = PhysicianServiceProxy.Current;
        _appointmentSvc = AppointmentServiceProxy.Current;

        appointments = _appointmentSvc.Appointments;

        foreach (var app in appointments)
        {
            app.Physician = _physicianSvc.Physicians.FirstOrDefault(p => p.Id == app.PhysicianId);
            app.Patient = _patientSvc.GetById(app.PatientId);
        }
    }

}
