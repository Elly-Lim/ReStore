using System;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // CreateHostBuilder(args).Build().Run(); 
           //startup code, execute when i run the application
           //remove the run command from this method and store the value that's return from this method
           //in a variable called host
                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
                //specify the service from our class called StoreContext
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>(); //ILogger I/F
                //log to the terminal any errors i get when i am doing this 
                //because i dont have an eccess to a developer exception page (app.UseDeveloperExceptionPage)
                try
                {
                    context.Database.Migrate(); 
                    //applies any pending migrations for the context to the database. Will create the satabase if it does not already exist
                    //Note that this API is mutually exclusive with MS Entity Framework Core Infrastructure Database Ensure Created
                    //dose not use migrations ??????
                    DbInitializer.Initialize(context); //Db name.method name(databasename);
                    //pass in the context as the argument? here so that this has access to our database context
                    //which it needs to use to run that method to at the products into our application
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Problem migrating data");
                } 
                host.Run();
               /* finally
                {
                    scope.Dispose(); //for garbage collection, free up the data
                    //simple way is using var scope = host.Services.CreateScope
                }*/
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
