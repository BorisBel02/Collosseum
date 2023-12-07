using Colloseum;
using Colloseum.Model;
using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .ConfigureServices(((hostContext, services) =>
            {
                services.AddHostedService<ExperimentWorkerSqlite>();
                services.AddScoped<ExperimentSqlite>();
                services.AddTransient<IFighter, Fighter>();
                services.AddSingleton<IGods, Gods>();
                services.AddSingleton<DbContext, Context>();
            }))
            .Build()
            .RunAsync();
    }
}
