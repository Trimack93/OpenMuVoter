using OpenMuVoter.Interfaces.Outputs;
using OpenMuVoter.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMuVoter.Outputs
{
    class ConsoleOutput : IOutput
    {
        public void Write(object message)
        {
            Console.Write(message);
        }

        public void WriteLine(object message)
        {
            Console.WriteLine(message);
        }

        public void WriteColor(object message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        public void WriteLineColor(object message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public string ReadPassword()
        {
            return PasswordMasker.ReadPasswordFromConsole();
        }
    }
}
