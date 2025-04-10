namespace Connector.Models
{
  public class RateList
  {
    private readonly List<Rate> _rates = new List<Rate>();

    public void AddRate(Rate rate) { 
      _rates.Add(rate);
    }
    public List<Rate> GetRates()
    {
      return _rates;
    }

    public Rate? GetRateById(int id)
    {
      return _rates.FirstOrDefault(item => item.ID == id);
    }
  }
}
