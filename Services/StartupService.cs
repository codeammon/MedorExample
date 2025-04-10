using Connector.Controllers;
using Connector.Factories;
using Connector.HangfireJobs;

namespace Connector.Services
{
    public class StartupService : IHostedService
    {
        public StartupService() {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            string rate = "24";
            var jobs = new JobController();
            jobs.ScheduleCoinDeskJob();

            jobs.ScheduleCNBJob();

      
            Task<string> rateRes = CNBJob.GetRateCNBAsync();
            rate = rateRes.Result;
            CZKFactory.Instance.CZKEUR = rate;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
