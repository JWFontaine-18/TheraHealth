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
                return instance;
            }
        }
    }

    public List<Patient?> Patients
    {
        get
        {
            return patients;
        }
    }

    public Patient CreatePatient(ApplicationManager manager)
    {
        Console.WriteLine("\n***CREATING NEW PATIENT***");

        string name = InputHelper.GetValidString("Enter name of Patient: ");
        string address = InputHelper.GetValidString("Enter Address of Patient: ");
        DateOnly birthday = InputHelper.GetValidDate("Enter Birthday of Patient (MM/DD/YYYY): ");
        Race race = InputHelper.GetValidEnum<Race>("Enter Race of Patient: ");
        Gender gender = InputHelper.GetValidEnum<Gender>("Enter Gender of Patient: ");
        string notes = InputHelper.GetValidString("Enter Medical Notes of Patient: ");

        Patient patient = new Patient(name, address, notes, race, gender, birthday);
        manager.AddPatient(patient);
        Console.WriteLine("Patient created successfully!");

        return patient;
    }

    public static void DisplayAllPatients(ApplicationManager manager)
    {
        Console.WriteLine("\n--- All Patients ---");
        var patients = manager.GetPatients();

        if (patients.Count == 0)
        {
            Console.WriteLine("No patients found.");
            return;
        }

        foreach (var patient in patients)
        {
            Console.WriteLine(patient);
        }
    }
    
    public int getAge(DateOnly bday)
    {
        int currentYear = DateTime.Now.Year;
        int age = currentYear - bday.Year;
        return age;
    }
}
