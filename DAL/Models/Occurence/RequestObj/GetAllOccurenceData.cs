namespace LoanApplication.DAL.Models.Occurence.RequestObj
{
    public class GetAllOccurenceData
    {
        public bool issuccessful { get; set; }
        public string? code { get; set; }
        public string message { get; set; } = string.Empty;
        public List<OccurenceModel>? occurences { get; set; }
    }
}
