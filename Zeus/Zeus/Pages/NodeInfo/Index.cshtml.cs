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
    public class IndexModel : PageModel
    {
        private readonly Zeus.Data.ZeusContext _context;

        public IndexModel(Zeus.Data.ZeusContext context)
        {
            _context = context;
        }

        public IList<ZResponse> ZResponse { get;set; }

        public async Task OnGetAsync()
        {
            // clear database in order to make sure only current node data is shown
            var tlist = _context.ZResponse.ToList();
            _context.ZResponse.RemoveRange(tlist);
            await _context.SaveChangesAsync();

            // add fresh nodes to database
            _context.ZResponse.AddRange(ApiUtils.getNodesInfo());
            await _context.SaveChangesAsync();

            ZResponse = await _context.ZResponse.ToListAsync();
        }
    }
}
