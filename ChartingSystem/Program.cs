using System;
using System.Text;

namespace ChartingSystem;

public class Program
{
    public static void Main(string[] args)
    {
        bool flag = false;
        var manager = new ApplicationManager();
        do
        {
            Console.WriteLine("C. Create New Application");
            Console.WriteLine("R. Display all Applications");
            Console.WriteLine("P. Display all Patients");
            Console.WriteLine("D. Display all Physicians");
            Console.WriteLine("Q. Quit");
            var userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "C":
                case "c":
                {
                    Console.WriteLine("***CREATING NEW PATIENT*** ");
                    Console.WriteLine("Enter name of Pateint: ");
                    string name = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter Address of Pateint: ");
                    string address = Console.ReadLine() ?? string.Empty;
                    DateOnly bDay;
                    while (true)
                    {
                        Console.WriteLine("Enter Birthday of Patient (MM/DD/YYYY): ");
                        string? bDayInput = Console.ReadLine();
                        if (DateOnly.TryParseExact(bDayInput, "MM/dd/yyyy", out bDay))
                        {
                            break;
                        }
                        Console.WriteLine("Invalid date format. Please use MM/DD/YYYY.");
                    }
                    Race race;
                    while (true)
                    {
                        Console.WriteLine("Enter Race of Pateint: ");
                        string raceInput = Console.ReadLine() ?? string.Empty;
                        if (Enum.TryParse(raceInput, out race))
                        {
                            break;
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append("White\n");
                        sb.Append("AfricanAmerican\n");
                        sb.Append("Asian\n");
                        sb.Append("Hispanic\n");
                        sb.Append("AmericanIndian\n");
                        sb.Append("MiddleEasternorNothernAfrican\n");
                        sb.Append("NativeHawaiian\n");
                        sb.Append("Other\n");
                        Console.WriteLine($"Please Enter Patients Race from these options: \n{sb}");
                    }

                    Gender gender;
                    while (true)
                    {
                        Console.WriteLine("Enter Gender of Pateint: ");
                        string genderInput = Console.ReadLine() ?? string.Empty;
                        if (Enum.TryParse(genderInput, out gender))
                        {
                            break;
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Male\n");
                        sb.Append("Female\n");
                        Console.WriteLine(
                            $"Please Enter Patients Gender from these options: \n{sb}"
                        );
                    }
                    Console.WriteLine("Enter Medical Notes of Pateint: ");
                    string notes = Console.ReadLine() ?? string.Empty;
                    Patient patient = new Patient(name, address, notes, race, gender, bDay);
                    manager.AddPatient(patient);
                    Console.WriteLine("***CREATING NEW PHYSICIAN*** ");
                    Console.WriteLine("Enter name of Physican: ");
                    string pName = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter license number of Physican: ");
                    string licenseInput = Console.ReadLine() ?? string.Empty;
                    int license = int.Parse(licenseInput);
                    Console.WriteLine("Enter Graduation Date of Physician MM/DD/YYYY: ");
                    string gradInput = Console.ReadLine() ?? string.Empty;
                    DateOnly grad = DateOnly.ParseExact(gradInput, "MM/dd/yyyy", null);
                    Console.WriteLine("Enter Specialization of Physician: ");
                    string specialization = Console.ReadLine() ?? string.Empty;
                    Physician physician = new Physician(pName, license, grad, specialization);
                    manager.AddPhysician(physician);
                    Console.WriteLine("***CREATING NEW BOOKING*** ");
                    Console.WriteLine("Select a Day and time: ");
                    string apptInput = Console.ReadLine() ?? string.Empty;
                    DateTime appointmentTime = DateTime.ParseExact(
                        apptInput,
                        "MM/dd/yyyy HH:mm",
                        null
                    );
                    if (
                        manager.isPhysicanAvailable(physician, appointmentTime)
                        && manager.isValidBooking(appointmentTime)
                    )
                    {
                        Booking booking = new Booking(patient, physician, appointmentTime);
                        manager.AddBooking(booking);
                    }
                    else
                        Console.WriteLine("Already booked");

                    break;
                }
                case "R":
                case "r":
                    Console.WriteLine("Applications");
                    Console.WriteLine(manager);
                    break;
                case "P":
                case "p":
                    var patients = manager.GetPatients();
                    Console.WriteLine("Patients:");
                    foreach (var p in patients)
                    {
                        Console.WriteLine(p); // override Patient.ToString() for nicer output
                    }
                    break;
                case "D":
                case "d":
                    var physicians = manager.GetPhyscians();
                    Console.WriteLine("Physcians:");
                    foreach (var p in physicians)
                    {
                        Console.WriteLine(p); // override Patient.ToString() for nicer output
                    }
                    break;
                case "Q":
                case "q":
                    flag = true;
                    break;
                default:
                    break;
            }
        } while (!flag);
    }
}
