using System;
using Library.TheraHealth.Models;

namespace Library.TheraHealth.Services;

public class PhysicianServiceProxy
{
    public List<Physician?> Physicians;
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

        if (physician.Id <= 0)
        {
            var maxId = -1;
            if (Physicians.Any())
            {
                maxId = Physicians.Select(b => b?.Id ?? -1).Max();
            }
            else
            {
                maxId = 0;
            }
            physician.Id = ++maxId;
            Physicians.Add(physician);
        }
        else
        {
            var phyToEdit = Physician.FirstOrDefault(b => (b?.Id ?? 0) == physician.Id);
            if (phyToEdit != null)
            {
                var index = Physician.IndexOf(phyToEdit);
                Physician.RemoveAt(index);
                Physicians.Insert(index, physician);
            }
        }
            return physician;
    }
    public Physician? Delete(int id)
    {
        //get object
        var PhysToDel = Physician
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        Physician.Remove(PhysToDel);

        return PhysToDel;
    }
}

