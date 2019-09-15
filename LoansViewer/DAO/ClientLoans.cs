using System.Collections.Generic;

namespace LoansViewer.DAO
{
    public class ClientLoans : MongoDbEntity
    {
        public string Phone { get; set; }

        public IEnumerable<LoanRequest> Requests { get; set; }


    }
}