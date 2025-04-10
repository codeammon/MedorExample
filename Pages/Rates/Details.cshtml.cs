using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Connector.Models;

namespace Connector.Pages.Rates
{
    public class DetailsModel : PageModel
    {
        private readonly Connector.Models.RatesContext _context;

        public DetailsModel(Connector.Models.RatesContext context)
        {
            _context = context;
        }

        public Rate Rate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates.FirstOrDefaultAsync(m => m.ID == id);
            if (rate == null)
            {
                return NotFound();
            }
            else
            {
                Rate = rate;
            }
            return Page();
        }
    }
}
