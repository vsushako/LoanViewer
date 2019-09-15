using System.Threading.Tasks;

namespace LoansViewer.Api
{
    public interface IApiRequest
    {
        /// <summary>
        /// Makes request for loans
        /// </summary>
        /// <param name="phone">phone number</param>
        /// <returns></returns>
        Task<string> GetLoans(string phone);
    }
}