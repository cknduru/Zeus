using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zeus.Data;
using Zeus.Models;

namespace Zeus
{
    public class EditModel : PageModel
    {
        private readonly Zeus.Data.ZeusContext _context;

        public EditModel(Zeus.Data.ZeusContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ZResponse ZResponse { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ZResponse = await _context.ZResponse.FirstOrDefaultAsync(m => m.ID == id);

            if (ZResponse == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ZResponse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZResponseExists(ZResponse.ID))
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

        private bool ZResponseExists(int id)
        {
            return _context.ZResponse.Any(e => e.ID == id);
        }
    }
}
