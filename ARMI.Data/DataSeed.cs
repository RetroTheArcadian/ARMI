using ARMI.Data.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace ARMI.Data
{
    public static class DataSeed
    {
        public static void SeedData(IServiceScope serviceScope)
        {
            var service = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            service.EnsureSeedClientData();
            service.EnsureSeedSystemData();
        }
    }
}
