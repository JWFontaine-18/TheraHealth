using System;
using Library.TheraHealth.Services;

namespace Library.TheraHealth.Models;

public class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public Patient? Patient { get; set; }

    public int PhysicianId { get; set; }

    public Physician? Physician { get; set; }

    public DateTime? DateTime { get; set; }
    public string Display
    {
        get
        {
            return ToString();
        }
    }

    public override string ToString()
    {
        if (Patient == null || Physician == null)
        {
            return $"{PatientId} with {PhysicianId}";
        }

        return $"{Patient.Name} with {Physician.Name}";



    }

    public Appointment()
    {

    }
    public Appointment(int id)
    {
        var apptCopy = AppointmentServiceProxy.Current.Appointments.FirstOrDefault(b => (b?.Id ?? 0) == id);

        if (apptCopy != null)
        {
            Id = apptCopy.Id;
            PatientId = apptCopy.PatientId;
            PhysicianId = apptCopy.PhysicianId;
        }
    }

    public static implicit operator Appointment(Patient v)
    {
        throw new NotImplementedException();
    }
}
