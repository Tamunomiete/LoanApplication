using LoanApplication.DAL.Models.Occurence;

namespace LoanApplication.DAL.Models.LoanProducts.ResponseObj
{
    public class GetAllLoanProductResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseDescription { get; set; }
        public List<LoanProductDropdowModel>? Products { get; set; }
    }
}
