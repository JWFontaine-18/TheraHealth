using System;
using Library.TheraHealth.Models;
namespace Library.TheraHealth.Services;

public class AppointmentServiceProxy
{
    public List<Appointment?> Appointments;

    // private PatientServiceProxy _patientSvc;
    // private PhysicianServiceProxy _physicianSvc;
    private AppointmentServiceProxy()
    {
        // _patientSvc = PatientServiceProxy.Current;
        // _physicianSvc = PhysicianServiceProxy.Current;
        Appointments = new List<Appointment?>
        {
            new Appointment {Id = 1
            , PatientId = 1
            , PhysicianId = 1
            // , Patient = _patientSvc.Patients.FirstOrDefault(p => p.Id == 1)
            // , Physician = _physicianSvc.Physicians.FirstOrDefault(p => p.Id == 1) 
            },
            new Appointment {Id = 2
            , PatientId = 2
            , PhysicianId = 2
            // , Patient = _patientSvc.Patients.FirstOrDefault(p => p.Id == 2)
            // , Physician = _physicianSvc.Physicians.FirstOrDefault(p => p.Id == 2) 
            },
        };
    }
    private static AppointmentServiceProxy? instance;
    private static object instanceLock = new object();
    public static AppointmentServiceProxy Current
    {
        get
        {
            lock(instanceLock)
            { 
                if (instance == null)
                {
                    instance = new AppointmentServiceProxy();
                }
            }

            return instance;
        }
    }
}

