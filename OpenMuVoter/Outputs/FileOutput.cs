using OpenMuVoter.Interfaces.Outputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMuVoter.Outputs
{
    class FileOutput : IOutput
    {
        private const string LogFileName = "OpenMuVoter_log.txt";
        private readonly string NewLine = Environment.NewLine;

        public FileOutput()
        {
            File.AppendAllText(LogFileName, "-----------------------------------------------" + NewLine);
            File.AppendAllText(LogFileName, "New voting session started: " + DateTime.Now + NewLine + NewLine);
        }

        ~FileOutput()
        {
            File.AppendAllText(LogFileName, NewLine + NewLine);
        }

        public void Write(object message)
        {
            File.AppendAllText(LogFileName, message.ToString());
        }

        public void WriteLine(object message)
        {
            File.AppendAllText(LogFileName, message + Environment.NewLine);
        }

        public void WriteColor(object message, ConsoleColor color)
        {
            Write(message);
        }

        public void WriteLineColor(object message, ConsoleColor color)
        {
            WriteLine(message);
        }

        public string ReadLine()
        {
            return "";
        }

        public string ReadPassword()
        {
            return "";
        }
    }
}
