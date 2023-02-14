using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChemsoftTest.Core.Database;

public class AppDbContext : DbContext
{
    // В реальных проектах подобные вещи хранятся в конфигурации или в секретах,
    // здесь эта строка размещена для простоты.
    private static readonly string ConnectionTemplate = 
        $"Host=localhost;Port=5432;Username={0};Password={1};Database={2}";

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    public async Task ConnectAsync(string username, string password, string dbName)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql();
    }
}