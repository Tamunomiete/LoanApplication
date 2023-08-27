using LoanApplication.DAL.Models;

namespace LoanApplication.DAL.RequestObj
{
    public class GetLoanApplicationData
    {
        public string? code { get; set; }
        public string? message { get; set; } 
        public tbl_LoanApplication? loanApplication { get; set; }
    }
}
