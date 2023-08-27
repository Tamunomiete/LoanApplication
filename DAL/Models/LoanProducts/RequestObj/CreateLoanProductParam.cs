namespace LoanApplication.DAL.Models.LoanProducts.RequestObj
{
    public class CreateLoanProductParam
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal MaximumInterestRate { get; set; }
        public decimal MinimumInterestRate { get; set; }
        public decimal MaximumLoanAmount { get; set; }
        public int MaximumLoanTenure { get; set; }
    }
}
