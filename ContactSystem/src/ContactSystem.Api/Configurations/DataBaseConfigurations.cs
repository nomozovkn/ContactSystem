using ContactSystem.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Api.Configurations;

public static class DataBaseConfigurations
{
    public static void ConfigureDB(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(connectionString));
    }
}
