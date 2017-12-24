using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMuVoter.Interfaces.Outputs
{
    interface IOutput
    {
        void Write(string message);
        void WriteLine(string message);
        void WriteColor(string message, ConsoleColor color);
        void WriteLineColor(string message, ConsoleColor color);
        string ReadLine();
        string ReadPassword();
    }
}
