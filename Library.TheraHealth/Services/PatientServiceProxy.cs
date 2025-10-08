using System;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class PatientServiceProxy
{
    public List<Patient?> Patients { get; set; }
    private PatientServiceProxy()
    {
        Patients = new List<Patient?>
        {
            new Patient {Id = 1, Name = "John Doe" },
            new Patient {Id = 2, Name = "Jane Doe" }
        };
    }
    private static PatientServiceProxy? instance;
    private static object instanceLock = new object();
    public static PatientServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new PatientServiceProxy();
                }
            }

            return instance;
        }
    }

    public Patient? GetById(int id)
    {
        if (id <= 0)
        {
            return null;
        }
        return Patients.FirstOrDefault(p => p.Id == id);
    }
}
