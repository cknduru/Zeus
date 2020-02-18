using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zeus.Data;
using Zeus.Models;

namespace Zeus
{
    public class CreateModel : PageModel
    {
        private readonly Zeus.Data.ZeusContext _context;

        public CreateModel(Zeus.Data.ZeusContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ZResponse ZResponse { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ZResponse.Add(ZResponse);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
