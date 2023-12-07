using Colloseum.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Colloseum;

public class ExperimentWorkerSqlite : IHostedService
{
    private readonly IHostApplicationLifetime _appLifeTime;
    
    private IExperiment _experiment;

    private DbContext _db;

    public ExperimentWorkerSqlite(
        IHostApplicationLifetime lifetime,
        ExperimentSqlite experiment,
        DbContext db)
    {
        _appLifeTime = lifetime;
        _experiment = experiment;
        _db = db;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            try
            {
                int iterations = 100;
                double wins = 0;
                for (int i = 0; i < iterations; ++i)
                {
                    if (_experiment.Run()) //how can i make experiment.Run() asynchronous?
                    {
                        ++wins;
                    }
                }

                _db.SaveChanges();
                Console.WriteLine("Wins = " + wins + " Iterations = " + iterations);
                Console.WriteLine("P = " + wins / iterations);
            }
            catch (TaskCanceledException){}
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                
                _appLifeTime.StopApplication();
            }
        });
        
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping experiments");
    }
}