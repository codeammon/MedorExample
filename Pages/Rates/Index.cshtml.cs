using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Connector.Models;

namespace Connector.Pages.Rates
{
    public class IndexModel : PageModel
    {
        private readonly RatesContext _context;

        public IndexModel(RatesContext context)
        {
            _context = context;
        }

        public List<Rate> Rates { get; set; } = default!;

        public async Task OnGetAsync()
        {
              Rates = await _context.Rates.ToListAsync();
        }
    }
}
