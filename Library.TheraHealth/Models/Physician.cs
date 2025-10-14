using System;

namespace Library.TheraHealth.Models;

public class Physician
{
    public string? Name { get; set; }
    
    public int? license { get; set; }

    public DateOnly grad { get; set; }
    
    public string? specialization { get; set; }
    public int? Id{get; set;}
}
