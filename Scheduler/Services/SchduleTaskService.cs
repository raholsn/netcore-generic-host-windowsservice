using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Scheduler.Services
{
    internal class SchduleTaskService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<string> _logger;
        private readonly ICleanupService _cleanup;

        public SchduleTaskService(ILogger<string> logger, ICleanupService cleanup)
        {
            _logger = logger;
            _cleanup = cleanup;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var message = "ScheduleTaskService doing work..";

            _logger.LogInformation(message);

            _cleanup.CleanUp();

            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(message, EventLogEntryType.Information, 101, 1);
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
}