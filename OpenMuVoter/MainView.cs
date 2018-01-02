using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenMuVoter.Utilities;
using OpenMuVoter.Interfaces.Outputs;
using OpenMuVoter.Outputs;

namespace OpenMuVoter
{
    class MainView
    {
        private readonly VotingBot _bot;
        private readonly IOutput _output;

        private string _login;
        private string _pass;

        public MainView(IOutput output, VotingBot bot)
        {
            _output = output;
            _bot = bot;
        }

        /// <summary>
        /// Checks whether user provided valid credentials.
        /// </summary>
        /// <param name="args">Command line arguments in format "user password".</param>
        public void ParseCredentials(string[] args)
        {
            if (args.Length < 2)
            {
                _output.Write("Please enter your user name: ");
                _login = _output.ReadLine();

                _output.Write("Please enter your password: ");
                _pass = _output.ReadPassword();
            }
            else
            {
                _login = args[0];
                _pass = args[1];
            }
        }

        /// <summary>
        /// Votes on sites and webshop. Also claims free 24h reward.
        /// </summary>
        public void Vote()
        {
            if (_bot.Login(_login, _pass))
            {
                VoteOnSites();
                VoteOnWebshop();
                Claim24hReward();
            }
            else
                throw new ArgumentException("Wrong username/password.");
        }

        /// <summary>
        /// Finishes program execution.
        /// </summary>
        public void Finish()
        {
            _output.WriteLine("Voting finished successfully!");

            if (_output is ConsoleOutput)
                _output.ReadLine();
        }

        private void VoteOnSites()
        {
            var votingSitesUrl = _bot.GetVotingSites();

            for (int i = 0; i < votingSitesUrl.Count; i++)
            {
                _output.Write($"Voting on site no. {i + 1}/{votingSitesUrl.Count}... ");

                bool result = _bot.VoteOnSite(votingSitesUrl[i]);

                if (result)
                    _output.WriteLineColor("done.", ConsoleColor.DarkGreen);
                else
                    _output.WriteLineColor("error.", ConsoleColor.Red);
            }
        }

        private void VoteOnWebshop()
        {
            _output.Write("Voting on WebShop... ");

            bool result = _bot.VoteOnWebshop();

            if (result)
                _output.WriteLineColor("done.", ConsoleColor.DarkGreen);
            else
                _output.WriteLineColor("error.", ConsoleColor.Red);
        }

        private void Claim24hReward()
        {
            _output.Write("Getting 24h free reward... ");

            bool canClaimReward = _bot.Check24hReward();

            if (canClaimReward)
            {
                string prize = _bot.Get24hReward();

                var color = prize.Contains("no reward this time.") ?
                    ConsoleColor.DarkYellow : ConsoleColor.DarkGreen;

                _output.WriteLineColor(prize, color);
            }
            else
                _output.WriteLineColor("error.", ConsoleColor.Red);
        }
    }
}
