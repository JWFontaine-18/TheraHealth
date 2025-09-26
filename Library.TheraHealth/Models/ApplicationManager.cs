using System;

namespace Library.TheraHealth.Models;

public class ApplicationManager
{
    private List<Booking> bookings = new List<Booking>();
    private List<Patient> patients = new List<Patient>();
    private List<Physician> physicians = new List<Physician>();

    public ApplicationManager(
        List<Booking>? bookings = null,
        List<Patient>? patients = null,
        List<Physician>? physicians = null
    )
    {
        this.bookings = bookings ?? new List<Booking>();
        this.patients = patients ?? new List<Patient>();
        this.physicians = physicians ?? new List<Physician>();
    }

    public bool isValidBooking(DateTime time)
    {
        if (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday)
        {
            return false;
        }
        if (time.Hour < 8 || time.Hour > 17)
        {
            return false;
        }
        return true;
    }

    public bool isPhysicanAvailable(Physician p, DateTime time)
    {
        foreach (Booking booking in bookings)
        {
            if (booking.Physician.name == p.name && booking.StartTime == time)
                return false;
        }
        return true;
    }

    public List<Booking> GetAllBookings() => bookings;

    public List<Patient> GetPatients() => patients;

    public List<Physician> GetPhyscians() => physicians;

    public void AddPatient(Patient p) => patients.Add(p);

    public void AddPhysician(Physician p) => physicians.Add(p);

    public void AddBooking(Booking b) => bookings.Add(b);

    public override string ToString()
    {
        string output = "";
        foreach (Booking b in bookings)
        {
            output +=
                $"PATIENT:\n{b.Patient.name} ({b.Patient.gender}, {b.Patient.race}) - DOB: {b.Patient.bDay:MM/dd/yyyy}\n";
            output +=
                $"PHYSICIAN:\n{b.Physician.name} {b.Physician.license}, {b.Physician.specialization} - Years of Experience: {b.Physician.GetYearsOfExperience()}\n";
            output += $"BOOKING:\n{b.Patient}\n{b.Physician}\n{b.StartTime}\n";
        }
        return output;
    }
}
