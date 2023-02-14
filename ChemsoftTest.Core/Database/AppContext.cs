using Microsoft.EntityFrameworkCore;

namespace ChemsoftTest.Core.Database;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql();
    }
}