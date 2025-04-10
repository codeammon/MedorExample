using Connector.Factories;
using Connector.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Connector.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RatesContext _context = new RatesContextFactory().CreateDbContext([]);
        private readonly ILogger<IndexModel> _logger;
        public string CZKEUR = "24";
        public IList<Rate> Rate { get; set; } = new List<Rate>();
        public Rate RateToAdd { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Rate = new List<Rate>();
            var rateList = RatesListFactory.GetRateList().GetRates();

            rateList.ForEach(rate => {
                var tmpRate = new Rate();
                tmpRate.ID = rate.ID;
                tmpRate.Code = rate.Code;
                tmpRate.Valid = rate.Valid;
                tmpRate.Value = Math.Round(float.Parse(rate.Value) * float.Parse(CZKEUR), 2).ToString();
                Rate.Add(tmpRate);
            });
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rateList = RatesListFactory.GetRateList().GetRates();
            rateList.ForEach(rate => {
                var tmpRate = new Rate();
                tmpRate.ID = rate.ID;
                tmpRate.Code = rate.Code;
                tmpRate.Valid = rate.Valid;
                tmpRate.Value = Math.Round(float.Parse(rate.Value) * float.Parse(CZKEUR), 2).ToString();
                Rate.Add(tmpRate);
            });

            var rate = rateList.Find(item => item.ID == id);
            if (rate != null)
            {
                var duplicity = _context.Rates.FirstOrDefault(i => i.Valid.ToString() == rate.Valid.ToString());

                if (duplicity != null)
                {
                    return BadRequest();
                }
                rate.ID = 0;
                RateToAdd = rate;
                _context.Rates.Add(RateToAdd);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
