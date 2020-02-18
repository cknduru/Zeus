using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zeus.Models;

namespace Zeus.Data
{
    public class ZeusContext : DbContext
    {
        public ZeusContext (DbContextOptions<ZeusContext> options)
            : base(options)
        {
        }

        public DbSet<Zeus.Models.ZResponse> ZResponse { get; set; }
    }
}
