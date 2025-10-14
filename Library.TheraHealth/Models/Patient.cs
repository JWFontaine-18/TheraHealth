using System;
using System.Net.Sockets;

namespace Library.TheraHealth.Models;

public class Patient
{
    public string? Name { get; set; }
    public int? Id { get; set; }

    public string? address { get; set; }

    public string? race {get; set;}
    public string? gender { get; set; }
    
    public DateOnly? bDay { get; set; }
}
