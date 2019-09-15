using System.Threading.Tasks;

namespace LoansViewer.Domain
{
    /// <summary>
    /// Loans methods
    /// </summary>
    public interface ILoan
    {
        /// <summary>
        /// Gets loans from external resource
        /// </summary>
        /// <param name="phone">phone number</param>
        /// <returns></returns>
        Task<string> GetLoans(string phone);
    }
}