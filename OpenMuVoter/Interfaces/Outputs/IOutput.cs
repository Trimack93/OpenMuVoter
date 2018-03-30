using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMuVoter.Interfaces.Outputs
{
    interface IOutput
    {
        void Write(object message);
        void WriteLine(object message);
        void WriteColor(object message, ConsoleColor color);
        void WriteLineColor(object message, ConsoleColor color);
        string ReadLine();
        string ReadPassword();
    }
}
