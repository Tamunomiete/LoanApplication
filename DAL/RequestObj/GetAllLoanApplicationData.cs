using LoanApplication.DAL.Models;

namespace LoanApplication.DAL.RequestObj
{
    public class GetAllLoanApplicationData
    {
        public string? code { get; set; }

        public string? message { get; set; }

        public List<tbl_LoanApplication>? loanApplications { get; set; }
    }
}
