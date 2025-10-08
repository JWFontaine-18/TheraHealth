using System;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class PhysicianServiceProxy
{
    public  List<Physician?> Physicians { get; set; }
    private PhysicianServiceProxy()
    {
        Physicians = new List<Physician?>
        {
            new Physician {Id = 1, Name = "John Doe" },
            new Physician {Id = 2, Name = "Jane Doe" }
        };
    }
    private static PhysicianServiceProxy? instance;
    private static object instanceLock = new object();
    public static PhysicianServiceProxy Current
    {
        get
        {
            lock(instanceLock)
            { 
                if (instance == null)
                {
                    instance = new PhysicianServiceProxy();
                }
            }

            return instance;
        }
    }
}

