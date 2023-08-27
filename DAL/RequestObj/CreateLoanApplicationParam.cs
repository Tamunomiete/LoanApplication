namespace LoanApplication.DAL.RequestObj
{
    public class CreateLoanApplicationParam
    {

        public string? BorrowerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BVNNumber { get; set; }
        public decimal LoanAmount { get; set; }

        public decimal Interest { get; set; }
        public int LoanTenure { get; set; }
        public int LoanRepaymentType { get; set; }

        public string? frequency { get; set; }

   

        public string? LoanProductCode { get; set; }

        public string? LoanPurpose { get; set; }

    }
}
