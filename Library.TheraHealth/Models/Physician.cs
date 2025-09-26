using System;

namespace Library.TheraHealth.Models;

public class Physician
{
    internal string? name { get; set; }
    internal int? license { get; set; }

    internal DateOnly graduation { get; set; }

    internal string specialization { get; set; }

    public Physician(string name, int license, DateOnly grad, string specialization)
    {
        this.name = name;
        this.license = license;
        this.graduation = grad;
        this.specialization = specialization;
    }

    public int GetYearsOfExperience()
    {
        return DateTime.Now.Year - graduation.Year;
    }

    public override string ToString()
    {
        return $"{name} {license}, {graduation}, {specialization} - Years of Experience: {GetYearsOfExperience()} ";
    }
}
