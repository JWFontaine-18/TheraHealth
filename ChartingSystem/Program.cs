using System;
using System.Text;
using Library.TheraHealth.Models;
using Library.TheraHealth.Services;

namespace ChartingSystem;

public class Program
{
    private static ApplicationManager manager = new ApplicationManager();

    public static void Main(string[] args)
    {
        List<Patient?> patients = PatientServiceProxy.Current.Patients;
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
                    PatientServiceProxy.DisplayAllPatients(manager);
                    break;
                case "D":
                    PhysicianServiceProxy.DisplayAllPhysicians(manager);
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
        Patient patient = PatientServiceProxy.Current.CreatePatient(manager);
        if (patient == null) return;

        Physician physician = PhysicianServiceProxy.Current.CreatePhysician(manager);
        if (physician == null) return;

        CreateBooking(patient, physician);
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

}
