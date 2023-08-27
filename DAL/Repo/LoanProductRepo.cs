using LoanApplication.DAL.Models;

namespace LoanApplication.DAL.Repo
{
    public class LoanProductRepo
    {
        private readonly LoanOriginationContext _context;
        private readonly IConfiguration _config;
        public LoanProductRepo(LoanOriginationContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }



    }
}
