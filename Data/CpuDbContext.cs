using Angular.SignalStore.InheritanceStructure.Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Angular.SignalStore.InheritanceStructure.Backend.Data;

public class CpuDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connection = "server=localhost; database=cpus; user=root; password=";
        
        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
    }
    
    public DbSet<Cpu>? Cpus { get; set; }
}