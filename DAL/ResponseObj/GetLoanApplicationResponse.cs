using LoanApplication.DAL.Models;

namespace LoanApplication.DAL.ResponseObj
{
    public class GetLoanApplicationResponse
    {
        public string? Responsecode { get; set; }
        public string? ResponseDescription { get; set; }
        public tbl_LoanApplication? loanApplication { get; set; }
    }
}
