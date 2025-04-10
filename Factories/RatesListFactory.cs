using Connector.Models;

namespace Connector.Factories
{
  public class RatesListFactory
  {
    private static readonly RateList _list = new RateList();

    public static RateList GetRateList()
    {
      return RatesListFactory._list;
    }
  }
}
