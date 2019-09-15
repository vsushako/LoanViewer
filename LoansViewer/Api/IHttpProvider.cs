using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LoansViewer.Api
{
    /// <summary>
    /// Client for http requests to bpm
    /// </summary>
    public interface IHttpProvider
    {
        /// <summary>
        /// Sends request to bpm
        /// </summary>
        /// <param name="url">message to send</param>
        /// <param name="json">token to cancel operatio</param>
        /// <returns></returns>
        Task<string> Post(string url, string json);
    }
}