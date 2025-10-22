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
    public bool IsValidAppointmentTime(DateTime? dateTime)
    {
        if (dateTime == null)
        {
            return false;
        }

        var dt = dateTime.Value;

        // Check if it's Monday-Friday (1-5)
        if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
        {
            return false;
        }

        // Check if time is between 8 AM and 5 PM
        if (dt.Hour < 8 || dt.Hour >= 17)
        {
            return false;
        }

        return true;
    }

    public bool IsPhysicianAvailable(int physicianId, DateTime? dateTime, int? excludeAppointmentId = null)
    {
        if (dateTime == null)
        {
            return true;
        }

        // Check if physician already has an appointment at this exact time
        var conflictingAppointment = appointments
            .Where(a => a != null)
            .Where(a => (a?.PhysicianId ?? 0) == physicianId)
            .Where(a => (a?.DateTime) == dateTime)
            .Where(a => excludeAppointmentId == null || (a?.Id ?? 0) != excludeAppointmentId)
            .FirstOrDefault();

        return conflictingAppointment == null;
    }

    public Appointment? AddOrUpdate(Appointment? appointment)
    {
        if (appointment == null)
        {
            return null;
        }

        // Validate appointment time
        if (!IsValidAppointmentTime(appointment.DateTime))
        {
            throw new Exception("Appointments must be scheduled Monday-Friday between 8 AM and 5 PM.");
        }

        // Check for physician availability
        if (!IsPhysicianAvailable(appointment.PhysicianId, appointment.DateTime, appointment.Id > 0 ? appointment.Id : null))
        {
            throw new Exception("This physician is already booked at the selected time.");
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

