using System;
using Library.TheraHealth.Models;
namespace Library.TheraHealth.Services;

public class PhysicianServiceProxy
{
    private List<Physician?> physicians;

    private PhysicianServiceProxy()
    {
        physicians = new List<Physician?>();
    }

    private static PhysicianServiceProxy? instance;

    private static object instanceLock = new object();

    public static PhysicianServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new PhysicianServiceProxy();
                }
                return instance;
            }
        }
    }

    public List<Physician?> Physicians
    {
        get
        {
            return physicians;
        }
    }
    public Physician CreatePhysician(ApplicationManager manager)
    {
        Console.WriteLine("\n***CREATING NEW PHYSICIAN***");
        
        string name = InputHelper.GetValidString("Enter name of Physician: ");
        int license = InputHelper.GetValidInteger("Enter license number of Physician: ");
        DateOnly graduationDate = InputHelper.GetValidDate("Enter Graduation Date of Physician (MM/DD/YYYY): ");
        string specialization = InputHelper.GetValidString("Enter Specialization of Physician: ");
        
        Physician physician = new Physician(name, license, graduationDate, specialization);
        manager.AddPhysician(physician);
        Console.WriteLine("Physician created successfully!");
        
        return physician;
    }

    public static void DisplayAllPhysicians(ApplicationManager manager)
    {
        Console.WriteLine("\n--- All Physicians ---");
        var physicians = manager.GetPhyscians();

        if (physicians.Count == 0)
        {
            Console.WriteLine("No physicians found.");
            return;
        }

        foreach (var physician in physicians)
        {
            Console.WriteLine(physician);
        }
    }

    
}
