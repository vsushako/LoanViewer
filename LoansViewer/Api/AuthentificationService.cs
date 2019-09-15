using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoansViewer.Api
{
    /// <summary>
    /// Authentification service for bpm 
    /// </summary>
    internal class AuthentificationService: IAuthentificationService
    {
        public AuthentificationService(string url, string userName, string userPassword)
        {
            Uri = new Uri(url);
            UserName = userName;
            UserPassword = userPassword;
        }

        // Store cookies to prevent multiple requests
        private static CookieContainer CookieContainer { get; set; } = new CookieContainer();
        
        private string UserName { get; }
        private string UserPassword { get; }
        private Uri Uri { get; }

        /// <summary>
        /// Return auth cookies for bpm
        /// </summary>
        /// <returns></returns>
        public CookieContainer GetAuthCookies()
        {
            if (!IsExpired() && CookieContainer.GetCookies(Uri).Count > 0) return CookieContainer;

            lock (CookieContainer)
            {
                if (!IsExpired() && CookieContainer.GetCookies(Uri).Count > 0) return CookieContainer;

                CookieContainer = AuthentificationRequest();
                return CookieContainer;
            }
        }

        /// <summary>
        /// Checks if cookie are expired
        /// </summary>
        /// <returns></returns>
        private bool IsExpired()
        {
            foreach (Cookie cookie in CookieContainer.GetCookies(Uri))
                if (cookie.Expired) return true;

            return false;
        }

        /// <summary>
        /// Request for cookie
        /// </summary>
        /// <returns></returns>
        private CookieContainer AuthentificationRequest()
        {
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler {CookieContainer = cookies};

            // due to this type of query will be very rare it could be made by using
            using (var client = new HttpClient(handler))
            {
                var authParams = new StringContent(
                    $"{{\"UserName\":\"{UserName}\", \"UserPassword\": \"{UserPassword}\"}}", Encoding.UTF8, "application/json");
                Task.WaitAll(client.PostAsync(Uri, authParams));
                return cookies;
            }
        }
    }

    internal interface IAuthentificationService
    {
        CookieContainer GetAuthCookies();
    }
}