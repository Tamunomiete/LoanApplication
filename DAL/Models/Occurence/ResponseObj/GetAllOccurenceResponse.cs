namespace LoanApplication.DAL.Models.Occurence.ResponseObj
{
    public class GetAllOccurenceResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseDescription { get; set; }
        public List<OccurenceModel>? occurence { get; set; }
    }
}
