namespace Connector.Factories
{
  public class CZKFactory
  {
    public string CZKEUR { get; set; }
    private static CZKFactory instance = null;
    private static readonly object padlock = new object();

    private CZKFactory() { }


    public static CZKFactory Instance
    {
      get
      {
        lock (padlock)
        {
          if (instance == null)
          {
            instance = new CZKFactory();
          }
          return instance;
        }
      }
    }
  }
}
