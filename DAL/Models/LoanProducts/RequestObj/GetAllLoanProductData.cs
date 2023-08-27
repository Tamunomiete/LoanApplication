using LoanApplication.DAL.Models.LoanProducts.ResponseObj;
using LoanApplication.DAL.Models.Occurence;

namespace LoanApplication.DAL.Models.LoanProducts.RequestObj
{
    public class GetAllLoanProductData
    {
        public bool issuccessful { get; set; }
        public string? code { get; set; }
        public string message { get; set; } = string.Empty;
        public List<LoanProductDropdowModel>? loanProducts { get; set; }
    }
}
