using System;
using System.Net.Sockets;
using Library.TheraHealth.Services;

namespace Library.TheraHealth.Models;

public class Patient
{
    public string? Name { get; set; }
    public int Id { get; set; }

    public string? address { get; set; }

    public string? race { get; set; }
    public string? gender { get; set; }

    public DateOnly? bDay { get; set; }

    public string Display
    {
        get
        {
            return ToString();
        }
    }

    public override string ToString()
    {
        if (Name == null)
        {
            return $"{Id}";
        }

        return $"{Id} - {Name}";
    }
    public Patient()
    {

    }
    public Patient(int id)
    {
        var patientCopy = PatientServiceProxy.Current.Patients.FirstOrDefault(b => (b?.Id ?? 0) == id);

        if (patientCopy != null)
        {
            Id = patientCopy.Id;
            Name = patientCopy.Name;
        }
    }

}
