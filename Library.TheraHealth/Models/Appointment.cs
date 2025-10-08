using System;

namespace Library.TheraHealth.Models;

public class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public Patient? Patient { get; set; }

    public int PhysicianId { get; set; }

    public Physician? Physician { get; set; }

    public DateTime? DateTime { get; set; }

    public override string ToString()
    {
        if (Patient == null || Physician == null)
        {
            return $"{DateTime}: {PatientId} with {PhysicianId}";
        }

        return $"{DateTime}: {Patient.Name} with {Physician.Name}";
        

    }
}
