using GovUkApi;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CoreGovUkTestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddScoped<IGovUkApi, GovUkApi.GovUkApi>().BuildServiceProvider();

            var api = serviceProvider.GetService<IGovUkApi>();

            var users = await api.GetUsersInArea();
        }
    }
}
