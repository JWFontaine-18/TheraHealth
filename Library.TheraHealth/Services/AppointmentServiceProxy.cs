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
        Appointments = new List<Appointment?>();
    }
    private static AppointmentServiceProxy? instance;
    private static object instanceLock = new object();
    public static AppointmentServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new AppointmentServiceProxy();
                }
            }

            return instance;
        }
    }

    public List<Appointment?> Appointment
    {
        get
        {
            return Appointments;
        }
    }
    public Appointment? AddOrUpdate(Appointment? appointment)
    {
        if (appointment == null)
        {
            return null;
        }
        
        return appointment;

    }
    public Appointment? Delete(int id)
    {
        //get blog object
        var apptToDel = Appointments
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        Appointments.Remove(apptToDel);

        return apptToDel;
    }
}

