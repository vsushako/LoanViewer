using System;
using System.Linq;
using System.Threading.Tasks;
using LoansViewer.Api;
using LoansViewer.DAO;

namespace LoansViewer.Domain
{
    public class Loan : ILoan
    {
        private readonly IApiRequest _apiRequest;
        private readonly IMongoDao<ClientLoans> _dao;

        public Loan(IApiRequest apiRequest, IMongoDao<ClientLoans> dao)
        {
            _dao = dao;
            _apiRequest = apiRequest;
        }

        public async Task<string> GetLoans(string phone)
        {
            var result = await _apiRequest.GetLoans(phone);

            var loan = await _dao.GetByCondition(c => c.Phone == phone);
            var loanRequest = new LoanRequest { Data = result, Date = DateTime.UtcNow };

            if (loan == null)
                loan = new ClientLoans
                {
                    Phone = phone,
                    Requests = new[] { loanRequest }
                };
            else
                loan.Requests.ToList().Add(loanRequest);

            await _dao.Save(loan).ConfigureAwait(false);
            return result;
        }
    }
}