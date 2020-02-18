using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zeus.Data;
using Zeus.Models;

namespace Zeus
{
    public class DeleteModel : PageModel
    {
        private readonly Zeus.Data.ZeusContext _context;

        public DeleteModel(Zeus.Data.ZeusContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ZResponse = await _context.ZResponse.FindAsync(id);

            if (ZResponse != null)
            {
                _context.ZResponse.Remove(ZResponse);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
