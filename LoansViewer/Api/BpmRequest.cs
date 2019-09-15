using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

namespace LoansViewer.Api
{
    public class BpmRequest : IApiRequest
    {
        private IHttpProvider HttpProvider { get; }

        private string Url { get; }

        public BpmRequest(IHttpProvider provider, string url)
        {
            HttpProvider = provider;
            Url = url;
        }

        /// <inheritdoc/>
        public async Task<string> GetLoans(string phone)
        {
            return await HttpProvider.Post(Url, $"{{\"mobile\":\"{phone}\"}}");
        }
    }
}