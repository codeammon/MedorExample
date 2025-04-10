using Connector.Factories;
using Connector.HangfireJobs;
using Connector.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Connector.Controllers
{
    public class JobController : Controller
    {
        private static RatesContextFactory ratesContextFactory = new RatesContextFactory();
        private RatesContext _dbContext = ratesContextFactory.CreateDbContext([]);
        public JobController() { }

        public async Task<ActionResult> Index()
        {
            return View(await _dbContext.Rates.ToListAsync());
        }

        public async Task<ActionResult> ScheduleCoinDeskJob()
        {
            var job = new CoinDeskJob();

            RecurringJob.AddOrUpdate<CoinDeskJob>("CoinDesk", (job) => job.Execute(null, true), "* * * * *", new RecurringJobOptions());

            return null;
        }

        public async Task<ActionResult> ScheduleCNBJob()
        {
            var job = new CNBJob();

            RecurringJob.AddOrUpdate<CNBJob>("CNB", (job) => job.Execute(null, true), "0 1 * * *", new RecurringJobOptions());

            return null;
        }
    }
}
