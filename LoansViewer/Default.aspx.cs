using System;
using System.IdentityModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.UI;
using Autofac.Integration.Web.Forms;
using LoansViewer.Domain;

namespace LoansViewer
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetLoans(string phone)
        {
            if (!Regex.IsMatch(phone, "^\\+639[0-9]{9}$")) throw new BadRequestException("Wrong number");
            return Task.Run(() =>
            {
                var loan = Bootstrapper.Resolve<ILoan>();
                return loan.GetLoans(phone);
            }).Result;
        }
    }
}