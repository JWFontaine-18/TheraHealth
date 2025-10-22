using System;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class PhysicianServiceProxy
{
    public List<Physician?> physicians;
    private PhysicianServiceProxy()
    {
        physicians = new List<Physician?>();
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
    
    public List<Physician?> Physicians
    {
        get
        {
            return physicians;
        }
    }

    public Physician? AddOrUpdate(Physician? physician)
    {
        if (physician == null)
        {
            return null;
        }

        if (physician.Id <= 0)
        {
            var maxId = -1;
            if (physicians.Any())
            {
                maxId = physicians.Select(b => b?.Id ?? -1).Max();
            }
            else
            {
                maxId = 0;
            }
            physician.Id = ++maxId;
            physicians.Add(physician);
        }
        else
        {
            var phyToEdit = Physicians.FirstOrDefault(b => (b?.Id ?? 0) == physician.Id);
            if (phyToEdit != null)
            {
                var index = Physicians.IndexOf(phyToEdit);
                Physicians.RemoveAt(index);
                physicians.Insert(index, physician);
            }
        }
            return physician;
    }
    public Physician? Delete(int id)
    {
        //get object
        var PhysToDel = Physicians
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        Physicians.Remove(PhysToDel);

        return PhysToDel;
    }
}

