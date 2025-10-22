using System;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class PatientServiceProxy
{
    private List<Patient?> patients;
    private PatientServiceProxy()
    {
        patients = new List<Patient?>();
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
    
    public List<Patient?> Patients
    {
        get
        {
            return patients;
        }
    }

    public Patient? AddOrUpdate(Patient? patient)
    {
        if (patient == null)
        {
            return null;
        }

        if (patient.Id <= 0)
        {
            var maxId = -1;
            if (Patients.Any())
            {
                maxId = Patients.Select(b => b?.Id ?? -1).Max();
            }
            else
            {
                maxId = 0;
            }
            patient.Id = ++maxId;
            Patients.Add(patient);
        }
        else
        {
            var patToEdit = Patients.FirstOrDefault(b => (b?.Id ?? 0) == patient.Id);
            if (patToEdit != null)
            {
                var index = Patients.IndexOf(patToEdit);
                Patients.RemoveAt(index);
                Patients.Insert(index, patient);
            }
        }
            return patient;
    }
    public Patient? Delete(int id)
    {
        //get object
        var PatToDel = patients
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        patients.Remove(PatToDel);

        return PatToDel;
    }
}
