using System.ComponentModel.DataAnnotations;

namespace LoanApplication.DAL.Models.Occurence
{
    public class tbl_occurence
    {
        [Key]
        [StringLength(3)]
        public string? occurencecode { get; set; }

        [StringLength(100)]
        public string? occurencename { get; set; }

        public int? Flag { get; set; }
    }
}
