using Microsoft.AspNetCore.Identity;
using Net9Auth.API.Database;
using Microsoft.EntityFrameworkCore;

namespace Net9Auth.API.Services.Register;

public static class DatabaseRegistration
{
    public static void RegisterDatabase(this WebApplicationBuilder webApplicationBuilder)
    {
        var connectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string not found");
        webApplicationBuilder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connectionString));
    }

    

}