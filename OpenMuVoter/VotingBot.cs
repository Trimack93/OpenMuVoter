using HttpHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace OpenMuVoter
{
    internal class VotingBot
    {
        private const int VOTE_TIMEOUT_MS = 250;

        private IList<string> _siteUrls = new List<string>();

        public VotingBot()
        {
            ReadVotingSitesUrl();
        }

        /// <summary>
        /// Gets voting sites URLs.
        /// </summary>
        /// <returns>Voting sites URLs.</returns>
        public IList<string> GetVotingSites()
        {
            return _siteUrls;
        }

        /// <summary>
        /// Login to the site using provided credentials.
        /// </summary>
        /// <param name="username">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>True, if user logged in successfully, otherwise false.</returns>
        public bool Login(string username, string password)
        {
            string postData = $"username={username}&password={password}&login=Login";
            string url = Properties.Resources.Vote_URL;

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest myRequest = HttpHelper.CreateRequest(url);
            myRequest.ContentLength = data.Length;

            using (Stream newStream = myRequest.GetRequestStream())
                newStream.Write(data, 0, data.Length);

            string response = HttpHelper.ReadResponse(myRequest);

            return !response.Contains("There is no such username or password is wrong");
        }

        /// <summary>
        /// Votes on specified site.
        /// </summary>
        /// <param name="url">URL of voting site.</param>
        /// <returns>True, if vote is successfull, otherwise false.</returns>
        public bool VoteOnSite(string url)
        {
            return Vote(url, "You won 15 credits");
        }

        /// <summary>
        /// Votes on WebShop.
        /// </summary>
        /// <returns>True, if vote is successfull, otherwise false.</returns>
        public bool VoteOnWebshop()
        {
            const string validationMessage = "Already voted! You can win new 25 credits After:";

            string validationUrl = Properties.Resources.WebShop_URL;
            string votingUrl = Properties.Resources.WebshopVote_URL;

            HttpWebRequest request = HttpHelper.CreateRequest(validationUrl);
            string result = HttpHelper.ReadResponse(request);

            if (result.Contains(validationMessage))
                return false;

            return Vote(votingUrl, validationMessage);
        }

        /// <summary>
        /// Checks if 24h reward is available.
        /// </summary>
        /// <returns>True, if reward can be claimed, otherwise false.</returns>
        public bool Check24hReward()
        {
            Random rng = new Random();

            string baseUrl = Properties.Resources.WebshopReward_URL;
            string testUrl = baseUrl + "&time=" + rng.NextDouble();

            HttpWebRequest request = HttpHelper.CreateRequest(testUrl);
            string result = HttpHelper.ReadResponse(request);

            return result.Contains("Get FREE Reward");
        }

        /// <summary>
        /// Gets the 24h reward.
        /// </summary>
        /// <returns>String containing reward value.</returns>
        public string Get24hReward()
        {
            Random rng = new Random();

            string baseUrl = Properties.Resources.WebshopReward_URL;
            string getUrl = baseUrl + "&get=true" + "&time=" + rng.NextDouble();

            HttpWebRequest request = HttpHelper.CreateRequest(getUrl);
            string result = HttpHelper.ReadResponse(request);

            int indexBegin = result.IndexOf("You won");
            int indexEnd = result.IndexOf("<img src");

            if (indexBegin != -1)
            {
                string prize = result.Substring(indexBegin, indexEnd - indexBegin);

                return prize;
            }

            return "no reward this time.";
        }

        /// <summary>
        /// Gets user's credits count.
        /// </summary>
        /// <returns>User's credits count.</returns>
        public int GetCreditsCount()
        {
            string url = Properties.Resources.GetCredits_URL;

            HttpWebRequest request = HttpHelper.CreateRequest(url);
            string serverResponse = HttpHelper.ReadResponse(request);

            Regex regex = new Regex(@"Credits: <span>[\d,]+</span>");
            string creditsAsHtml = regex.Match(serverResponse).Value;

            creditsAsHtml = creditsAsHtml.Replace("Credits: <span>", string.Empty);
            creditsAsHtml = creditsAsHtml.Replace("</span>", string.Empty);
            creditsAsHtml = creditsAsHtml.Replace(",", string.Empty);

            return Int32.Parse(creditsAsHtml);
        }

        private bool Vote(string url, string validResponse)
        {
            HttpWebRequest request = HttpHelper.CreateRequest(url);
            string result = HttpHelper.ReadResponse(request);

            Thread.Sleep(VOTE_TIMEOUT_MS);                                     // coby za bota nie uznali xD

            return result.Contains(validResponse);
        }
        
        private void ReadVotingSitesUrl()
        {
            using (var reader = new StringReader(Properties.Resources.VotingSites_URLs))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                    _siteUrls.Add(line);
            }
        }
    }
}
