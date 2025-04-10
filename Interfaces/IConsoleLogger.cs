using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connector.Interface
{
    public interface IConsoleLogger
    {
        void WriteLine(string line);
        void WriteLine(object obj);
    }
}
