using System;
using Library.TheraHealth.Models;
namespace Library.TheraHealth.Services;

public class AppointmentServiceProxy
{
    public List<Appointment?> appointments;

    // private AppointmentServiceProxy _appointmentSvc;
    // private PhysicianServiceProxy _physicianSvc;
    private AppointmentServiceProxy()
    {
        appointments = new List<Appointment?>();
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
            return appointments;
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
            if (appointments.Any())
            {
                maxId = appointments.Select(b => b?.Id ?? -1).Max();
            }
            else
            {
                maxId = 0;
            }
            appointment.Id = ++maxId;
            appointments.Add(appointment);
        }
        else
        {
            var appToEdit = appointments.FirstOrDefault(b => (b?.Id ?? 0) == appointment.Id);
            if (appToEdit != null)
            {
                var index = appointments.IndexOf(appToEdit);
                appointments.RemoveAt(index);
                appointments.Insert(index, appointment);
            }
        }
            return appointment;
    }
    public Appointment? Delete(int id)
    {
        //get object
        var apptToDel = appointments
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        appointments.Remove(apptToDel);

        return apptToDel;
    }
}

