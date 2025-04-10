using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Connector.Models;

namespace Connector.Data
{
    public class ConnectorContext : DbContext
    {
        public ConnectorContext (DbContextOptions<ConnectorContext> options)
            : base(options)
        {
        }

        public DbSet<Connector.Models.Rate> Rate { get; set; } = default!;
    }
}
