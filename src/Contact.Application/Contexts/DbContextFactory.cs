using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Contacts.Application.Contexts;
public static class DbContextFactory
{
    public static AppDbContext CreateDbContext(IConfiguration configuration)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
