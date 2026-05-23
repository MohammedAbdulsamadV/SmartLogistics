using Logistics.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Logistics.Infrastucture;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        // حط الـ Connection String بتاعتك هنا يدوياً للـ Migration فقط
        optionsBuilder.UseSqlServer("Server=localhost;Database=LogisticsDataBase;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;");

        return new ApplicationDbContext(optionsBuilder.Options, null);
    }
}