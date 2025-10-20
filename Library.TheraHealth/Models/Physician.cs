using System;
using Library.TheraHealth.Services;

namespace Library.TheraHealth.Models;

public class Physician
{
    public string? Name { get; set; }
    
    public int? license { get; set; }

    public DateOnly grad { get; set; }
    
    public string? specialization { get; set; }
    public int Id { get; set; }
    public Physician()
    {

    }
    public Physician(int id)
    {
        var physicianCopy = PhysicianServiceProxy.Current.Physicians.FirstOrDefault(b => (b?.Id ?? 0) == id);

        if (physicianCopy != null)
        {
            Id = physicianCopy.Id;
            Name = physicianCopy.Name;
        }
    }
}
