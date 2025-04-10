using Microsoft.EntityFrameworkCore;

namespace Connector.Models
{
  public class RatesContext : DbContext
  {
    public RatesContext(DbContextOptions<RatesContext> options)
        : base(options)
    {
    }

    public DbSet<Rate> Rates { get; set; }
  }
}
