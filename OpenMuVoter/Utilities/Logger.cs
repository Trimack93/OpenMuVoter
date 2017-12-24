using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMuVoter.Utilities
{
    static class Logger
    {
        private const string ErrorLogFileName = "OpenMuVoter_ErrorLog.txt";

        public static void LogError(string message)
        {
            File.AppendAllText(ErrorLogFileName, DateTime.Now + "\tError occured: " + message + Environment.NewLine);
        }
    }
}
