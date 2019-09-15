using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoansViewer.Api
{
    /// <inheritdoc />
    internal class BpmHttpProvider : IHttpProvider
    {
        private static readonly HttpClient HttpClient = new HttpClient(new HttpClientHandler { UseCookies = false });

        private IAuthentificationService AuthService { get; }

        public BpmHttpProvider(IAuthentificationService authService)
        {
            AuthService = authService;
        }

        /// <summary>
        /// Sends request to bpm
        /// </summary>
        /// <param name="requestMessage">message to send</param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage requestMessage)
        {
            return await HttpClient.SendAsync(requestMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends request with auth cookies
        /// </summary>
        /// <param name="requestMessage">message to send</param>\
        /// <returns></returns>
        private async Task<HttpResponseMessage> SendAuthRequest(HttpRequestMessage requestMessage)
        {
            var cookiesStr = new StringBuilder();
            var cookieContainer = AuthService.GetAuthCookies();
            var cookies = cookieContainer.GetCookies(requestMessage.RequestUri);

            foreach (Cookie cookie in cookieContainer.GetCookies(requestMessage.RequestUri))
                cookiesStr.Append($"{cookie.Name}={cookie.Value};");

            requestMessage.Content.Headers.Add("BPMCSRF", cookies["BPMCSRF"]?.Value);
            requestMessage.Content.Headers.Add("Cookie", cookiesStr.ToString());

            return await SendRequest(requestMessage);
        }

        public async Task<string> Post(string url, string json)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            var response = await SendAuthRequest(requestMessage);

            return await response.Content.ReadAsStringAsync();

        }
    }
}