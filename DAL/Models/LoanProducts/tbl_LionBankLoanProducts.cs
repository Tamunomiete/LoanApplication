using System.ComponentModel.DataAnnotations;

namespace LoanApplication.DAL.Models.LoanProducts
{
    public class tbl_LionBankLoanProducts
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? MaximumInterestRate { get; set; }
        public decimal? MinimumInterestRate { get; set; }
        public decimal? MaximumLoanAmount { get; set; }
        public int? MaximumLoanTenure { get; set; }
        public int Flag { get; set; }
    }
}
