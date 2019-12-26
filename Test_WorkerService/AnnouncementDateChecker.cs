using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Test_WorkerService
{
    public class AnnouncementDateChecker : BackgroundService
    {
        private readonly ILogger<AnnouncementDateChecker> _logger;
        
        public AnnouncementDateChecker(ILogger<AnnouncementDateChecker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("MADAFAKA - - - - - - - Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                var client = new HttpClient();
                _logger.LogInformation( await client.GetAsync(
                    new Uri("https://wa-piri-logistics-notification.azurewebsites.net/api/Notification?test=true"),
                    stoppingToken).Result.Content.ReadAsStringAsync(), DateTimeOffset.Now);   

            }
            
        }
    }
}