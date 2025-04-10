using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Connector.Models;

namespace Connector.Factories
{
  public class RatesContextFactory : IDesignTimeDbContextFactory<RatesContext>
  {
    public RatesContext CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();
      string connectionString = configuration.GetConnectionString("ConnectorContext") ?? throw new InvalidOperationException("Connection string 'ConnectorContext' not found.");
      var optionsBuilder = new DbContextOptionsBuilder<RatesContext>();
      optionsBuilder.UseSqlServer(connectionString);

      return new RatesContext(optionsBuilder.Options);
    }
  }
}
