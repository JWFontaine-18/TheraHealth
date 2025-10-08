using System;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class BookingsServiceProxy
{
    private List<Booking?> bookings;

    private BookingsServiceProxy()
    {
        bookings = new List<Booking?>();
    }

    private static BookingsServiceProxy? instance;
    private static object instanceLock = new object();

    public static BookingsServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new BookingsServiceProxy();
                }
                return instance;
            }
        }
    }

    public void CreateBooking(Patient patient, Physician physician, ApplicationManager manager)
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

}
