using System;

namespace ChartingSystem;

public class Booking
{
    public Patient Patient { get; private set; }
    public Physician Physician { get; private set; }
    public DateTime StartTime { get; private set; }

    public Booking(Patient patient, Physician physician, DateTime startTime)
    {
        Patient = patient;
        Physician = physician;
        StartTime = startTime;
    }
}
