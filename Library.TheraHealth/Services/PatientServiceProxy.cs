using System;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class PatientServiceProxy
{
    public List<Patient?> Patients { get; set; }
    private PatientServiceProxy()
    {
        Patients = new List<Patient?>();
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
    
    public List<Patient?> Patient
    {
        get
        {
            return Patient;
        }
    }

    public Patient? AddOrUpdate(Patient? patient)
    {
        if (patient == null)
        {
            return null;
        }
        
        return patient;

    }
    public Patient? Delete(int id)
    {
        //get blog object
        var PatToDel = Patient
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        Patient.Remove(PatToDel);

        return PatToDel;
    }
}
