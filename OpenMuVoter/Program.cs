using OpenMuVoter.Interfaces.Outputs;
using OpenMuVoter.Outputs;
using OpenMuVoter.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenMuVoter
{
    class Program
    {
        static void Main(string[] args)
        {
            ChangeFloatingPointCulture();

            IOutput programOutput = new ConsoleOutput();
            VotingBot bot = new VotingBot();
            MainView view = new MainView(programOutput, bot);

            try
            {
                view.ParseCredentials(args);
                view.Vote();
                view.Finish();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);
            }

            programOutput.ReadLine();
        }

        /// <summary>
        /// Changes , to . in double.toString().
        /// </summary>
        private static void ChangeFloatingPointCulture()
        {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = customCulture;
        }
    }
}
