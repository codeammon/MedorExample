using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Connector.Models;

namespace Connector.Pages.Rates
{
    public class EditModel : PageModel
    {
        private readonly Connector.Models.RatesContext _context;

        public EditModel(Connector.Models.RatesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rate Rate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate =  await _context.Rates.FirstOrDefaultAsync(m => m.ID == id);
            if (rate == null)
            {
                return NotFound();
            }
            Rate = rate;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Rate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(Rate.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RateExists(int id)
        {
            return _context.Rates.Any(e => e.ID == id);
        }
    }
}
