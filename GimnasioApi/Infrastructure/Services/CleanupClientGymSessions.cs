using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

public class CleanupClientGymSessions : IHostedService, IDisposable
{
    private readonly IServiceProvider _provider;
    private Timer _timer;

    public CleanupClientGymSessions(IServiceProvider provider)
    {
        _provider = provider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Ejecuta inmediatamente y luego cada hora
        _timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private void Callback(object state)
    {
        using (var scope = _provider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var now = DateTime.UtcNow;

            var sessions = dbContext.GymSessions
                .Include(s => s.ClientGymSessions)
                .Where(s => s.IsCancelled || s.SessionDate <= now)
                .ToList();

            foreach (var session in sessions)
            {
                dbContext.ClientGymSessions.RemoveRange(session.ClientGymSessions);
            }
            dbContext.SaveChanges();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}