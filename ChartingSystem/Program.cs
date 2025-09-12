using System;
using System.Text;

namespace ChartingSystem;

public class Program
{
    private static ApplicationManager manager = new ApplicationManager();

    public static void Main(string[] args)
    {
        bool continueRunning = true;
        
        while (continueRunning)
        {
            DisplayMenu();
            string userChoice = Console.ReadLine() ?? string.Empty;
            
            switch (userChoice.ToUpper())
            {
                case "C":
                    CreateNewApplication();
                    break;
                case "R":
                    DisplayAllApplications();
                    break;
                case "P":
                    DisplayAllPatients();
                    break;
                case "D":
                    DisplayAllPhysicians();
                    break;
                case "Q":
                    continueRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("\n--- Charting System Menu ---");
        Console.WriteLine("C. Create New Application");
        Console.WriteLine("R. Display all Applications");
        Console.WriteLine("P. Display all Patients");
        Console.WriteLine("D. Display all Physicians");
        Console.WriteLine("Q. Quit");
        Console.Write("Enter your choice: ");
    }

    private static void CreateNewApplication()
    {
        Patient patient = CreatePatient();
        if (patient == null) return;

        Physician physician = CreatePhysician();
        if (physician == null) return;

        CreateBooking(patient, physician);
    }

    private static Patient CreatePatient()
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

    private static Physician CreatePhysician()
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

    private static void CreateBooking(Patient patient, Physician physician)
    {
        Console.WriteLine("\n***CREATING NEW BOOKING***");
        
        DateTime appointmentTime;
        while (true)
        {
            appointmentTime = InputHelper.GetValidDateTime("Enter appointment date and time (MM/DD/YYYY HH:MM): ");
            
            if (manager.isPhysicanAvailable(physician, appointmentTime) && manager.isValidBooking(appointmentTime))
            {
                break;
            }
            
            Console.WriteLine("Sorry, that time slot is already booked or invalid. Please choose a different date and time.");
        }
        
        Booking booking = new Booking(patient, physician, appointmentTime);
        manager.AddBooking(booking);
        Console.WriteLine("Booking created successfully!");
    }


    private static void DisplayAllApplications()
    {
        Console.WriteLine("\n--- All Applications ---");
        Console.WriteLine(manager);
    }

    private static void DisplayAllPatients()
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

    private static void DisplayAllPhysicians()
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
