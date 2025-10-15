using System;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class PhysicianServiceProxy
{
    public  List<Physician?> Physicians { get; set; }
    private PhysicianServiceProxy()
    {
        Physicians = new List<Physician?>();
    }
    private static PhysicianServiceProxy? instance;
    private static object instanceLock = new object();
    public static PhysicianServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new PhysicianServiceProxy();
                }
            }

            return instance;
        }
    }
    
    public List<Physician?> Physician
    {
        get
        {
            return Physician;
        }
    }

    public Physician? AddOrUpdate(Physician? physician)
    {
        if (physician == null)
        {
            return null;
        }
        
        return physician;

    }
    public Physician? Delete(int id)
    {
        //get blog object
        var PhysToDel = Physician
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        Physician.Remove(PhysToDel);

        return PhysToDel;
    }
}

