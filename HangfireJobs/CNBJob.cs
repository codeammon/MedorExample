using Connector.Factories;
using Connector.Models;
using Hangfire.Logging;
using Hangfire.Server;
using System.Net.Http;

namespace Connector.HangfireJobs
{

    public class CNBJob 
    {
        private ILog _logger;

        public void Execute(PerformContext context, bool morning)
        {

            this._logger = new HangFireLogProvider().GetLogger("Info");

            try
            {
                var rateRes = GetRateCNBAsync();
                string rate = rateRes.Result;
                CZKFactory.Instance.CZKEUR = rate; 

                _logger.Info(string.Format(rate));
            }
            catch (Exception ex)
            {
                _logger.Info(ex.ToString());
            }
        }

        public static async Task<string> GetRateCNBAsync()
        {
            var rate = "24";
            HttpClient client = new HttpClient();
            string rates = await client.GetStringAsync("https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt?date=" + DateTime.Now.ToString("d.M.yyyy"));
            string[] cnbArray = rates.Split("\n");
            List<string> cnbList = new List<string>(cnbArray);

            var eurLine = cnbList.FirstOrDefault(val => val.Contains("EUR"));
            if (eurLine != null)
            {
                rate = eurLine.Split("|")[4];
            }

            return rate;
        }

    }
}
