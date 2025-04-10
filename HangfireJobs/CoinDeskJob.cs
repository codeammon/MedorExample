using Connector.Factories;
using Connector.Models;
using Hangfire.Logging;
using Hangfire.Server;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace Connector.HangfireJobs
{
    public class CoinDeskJob
    {
        private ILog _logger;
        private static RateList _rateList = RatesListFactory.GetRateList();
        private static string CODE = "BTC-EUR";

        public void Execute(PerformContext context, bool morning)
        {
            this._logger = new HangFireLogProvider().GetLogger("Info");

            try
            {
                var rate = GetRateAsync();
                _rateList.AddRate(rate.Result);
                _logger.Info(string.Format(CODE, rate.Result.Value));
            }
            catch (Exception ex)
            {
                _logger.Info(ex.ToString());
            }
        }

        static async Task<Rate> GetRateAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(
                "https://data-api.coindesk.com/spot/v1/latest/tick?market=coinbase&instruments=BTC-EUR");
            response.EnsureSuccessStatusCode();
      
            var rate = new Rate();
            rate.ID = _rateList.GetRates().Count() + 1;
            rate.Value = GetValueFromRateResponse(await response.Content.ReadAsStringAsync());
            rate.Code = CODE;
            rate.Valid = DateTime.Now;
      // return URI of the created resource.
            return rate;
    }

        private static string GetValueFromRateResponse(string res) {
            dynamic json = JsonConvert.DeserializeObject(res);
            return json.Data["BTC-EUR"]["PRICE"];
        }
    }
}
