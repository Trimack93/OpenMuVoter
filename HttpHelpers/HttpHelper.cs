using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpHelpers
{
    public static class HttpHelper
    {
        private static CookieContainer CookieContainer { get; set; } = new CookieContainer();
        
        /// <summary>
        /// Creates request for specified URL.
        /// </summary>
        /// <param name="url">URL of the site for which the request will be created.</param>
        /// <returns>HttpWebRequest for specified URL.</returns>
        public static HttpWebRequest CreateRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = CookieContainer;

            return request;
        }

        /// <summary>
        /// Gets a web response from the provided web request.
        /// </summary>
        /// <param name="request">Web request for which we want to recieve response.</param>
        /// <returns>String containing response in HTML.</returns>
        public static string ReadResponse(HttpWebRequest request)
        {
            using ( var response = request.GetResponse() )
            using ( var responseStream = response.GetResponseStream() )
            using ( var responseReader = new StreamReader(responseStream) )
            {
                return responseReader.ReadToEnd();
            }
        }

        /// <summary>
        /// Extracts &lt;table&gt; node from HTML document.
        /// </summary>
        /// <param name="htmlDocument">String containing HTML document to be parsed.</param>
        /// <returns>Inner HTML of &lt;table&gt; node.</returns>
        public static string GetTableFromHTML(string htmlDocument)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load( new StringReader(htmlDocument) );

            HtmlNode tableNode = doc.DocumentNode.SelectSingleNode("//table");

            return tableNode.InnerHtml;
        }

        /// <summary>
        /// Extracts node with announcements from HTML document.
        /// </summary>
        /// <param name="htmlDocument">String containing HTML document to be parsed.</param>
        /// <returns>Inner HTML of the node with announcements.</returns>
        public static string GetAnnouncementsFromHTML(string htmlDocument)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load( new StringReader(htmlDocument) );

            HtmlNode tableNode = doc.DocumentNode.SelectSingleNode("//section[@class='span8 pull-right' and @id='region-main']");

            return tableNode.InnerHtml;
        }
    }
}
