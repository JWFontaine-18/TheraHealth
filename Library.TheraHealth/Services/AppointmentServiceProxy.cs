using System;
using Library.TheraHealth.Models;
namespace Library.TheraHealth.Services;

public class AppointmentServiceProxy
{
    public List<Appointment?> Appointments;

    // private AppointmentServiceProxy _appointmentSvc;
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

        if (appointment.Id <= 0)
        {
            var maxId = -1;
            if (Appointments.Any())
            {
                maxId = Appointments.Select(b => b?.Id ?? -1).Max();
            }
            else
            {
                maxId = 0;
            }
            appointment.Id = ++maxId;
            Appointments.Add(appointment);
        }
        else
        {
            var appToEdit = Appointments.FirstOrDefault(b => (b?.Id ?? 0) == appointment.Id);
            if (appToEdit != null)
            {
                var index = Appointments.IndexOf(appToEdit);
                Appointments.RemoveAt(index);
                Appointments.Insert(index, appointment);
            }
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

