using Microsoft.EntityFrameworkCore;
using Net9Auth.API.Database;

namespace Net9Auth.API.Services.Registration;

public static class DatabaseRegistration
{
    public static void SetupDatabase(this WebApplicationBuilder webApplicationBuilder)
    {
        var connectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string not found");
        webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
    }

    

}