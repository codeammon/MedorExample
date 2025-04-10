using Connector.Interface;
using Hangfire.Logging;
using LogLevel = Hangfire.Logging.LogLevel;

namespace Connector.Models
{
  public class HangfireConsoleLogger : ILog, IConsoleLogger
  {
    public string Name { get; set; }

    public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null)
    {
      if (messageFunc == null)
      {
        // Before calling a method with an actual message, LogLib first probes
        // whether the corresponding log level is enabled by passing a `null`
        // messageFunc instance.
        return logLevel > LogLevel.Info;
      }

      // Writing a message somewhere, make sure you also include the exception parameter,
      // because it usually contain valuable information, but it can be `null` for regular
      // messages.
      Console.WriteLine(String.Format("{0}: {1} {2} {3}", logLevel, Name, messageFunc(), exception));

      // Telling LibLog the message was successfully logged.
      return true;
    }

    public void WriteLine(string line) => Log(LogLevel.Info, () => line);

    public void WriteLine(object obj) => Log(LogLevel.Info, messageFunc: obj.ToString);
  }

  public class HangFireLogProvider : ILogProvider
  {
    public ILog GetLogger(string name) => new HangfireConsoleLogger { Name = name };
  }
}