using Angular.SignalStore.InheritanceStructure.Backend.Data;
using Angular.SignalStore.InheritanceStructure.Backend.Interfaces;

namespace Angular.SignalStore.InheritanceStructure.Backend.Services;

public class SCore
{
    private readonly CpuDbContext _dbContext;

    public SCore(CpuDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Cpu>? All() => _dbContext.Cpus?.ToList();

    public Cpu? Get(int id) => _dbContext.Cpus?.Find(id);

    public bool New(Cpu entity)
    {
        try
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Cpu Update(int id, Cpu entity)
    {
        var cpu =  _dbContext.Cpus?.Find(id);
        if (cpu == null)
        {
            throw new Exception("Cpu not found");
        }
        
        cpu.Name = entity.Name;
        cpu.Manufacturer = entity.Manufacturer;
        cpu.Function = entity.Function;
        _dbContext.SaveChangesAsync();

        return cpu;
    }

    public void Delete(int id)
    {
        if (_dbContext.Cpus?.Find(id) == null)
        {
            throw new Exception("Cpu not found");
        }
        
        _dbContext.Cpus.Remove(_dbContext.Cpus.Find(id));
        _dbContext.SaveChanges();
    }
}