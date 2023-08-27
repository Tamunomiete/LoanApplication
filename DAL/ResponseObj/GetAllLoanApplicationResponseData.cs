using LoanApplication.DAL.Models;

namespace LoanApplication.DAL.ResponseObj
{
    public class GetAllLoanApplicationResponseData
    {
        public  string? Responsecode { get; set; }

        public string? Responsedescription { get; set; }

        public List<tbl_LoanApplication>? applications { get; set; }
    }
}
