using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities.Config;
using ApplicationCore.Interfaces.Logging;

namespace Infrastructure.Data
{
    public class ApplicationDbContextSeed 
    {
        public static async Task SeedAsync(ApplicationDbContext applicationDbContext, IAppLogger<ApplicationDbContext> appLogger, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!applicationDbContext.ClientApi.Any())
                {
                    applicationDbContext.ClientApi.AddRange(GetClientApi());

                    await applicationDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    appLogger.LogError(ex.Message.ToString());

                    await SeedAsync(applicationDbContext, appLogger, retryForAvailability);
                }
                throw;
            }
        }

        static IEnumerable<ClientApi> GetClientApi()
        {
            var clientApis = new List<ClientApi>();

            var clientApiDefault = new ClientApi {
                Name = "ToDo App",
                ClientId = "ToDoApp",
                ClientSecret = "ToDoApp",
                IsActive = true
            };
            clientApis.Add(clientApiDefault);

            return clientApis;
        }
    }
}