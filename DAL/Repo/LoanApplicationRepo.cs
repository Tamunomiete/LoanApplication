using LoanApplication.DAL.Models;
using LoanApplication.DAL.RequestObj;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LoanApplication.DAL.Repo
{
    public class LoanApplicationRepo
    {
        private readonly LoanOriginationContext _context;
        private readonly IConfiguration _config;
        public LoanApplicationRepo(LoanOriginationContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public CreateLoanApplicationData CreateLoanApplication(CreateLoanApplicationParam param)
        {
            try
            {
                var borrowerNameParam = new SqlParameter("@BorrowerName", param.BorrowerName);
                var phoneNumberParam = new SqlParameter("@PhoneNumber", param.PhoneNumber);
                var bvnNumberParam = new SqlParameter("@BVNNumber", param.BVNNumber);
                var loanAmountParam = new SqlParameter("@LoanAmount", param.LoanAmount);
                var loanTenureParam = new SqlParameter("@LoanTenure", param.LoanTenure);
                var loanRepaymentTypeParam = new SqlParameter("@LoanRepaymentType", param.LoanRepaymentType);
                var interestParam = new SqlParameter("@Interest", param.Interest);
                var frequencyParam = new SqlParameter("@frequency", param.frequency);
                var loanApplicationIdParam = new SqlParameter
                {
                    ParameterName = "@LoanApplicationId",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 10,
                    Direction = ParameterDirection.Output
                };
                var retValParam = new SqlParameter
                {
                    ParameterName = "@retVal",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                var retMsgParam = new SqlParameter
                {
                    ParameterName = "@retMsg",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Direction = ParameterDirection.Output
                };

                _context.Database.ExecuteSqlRaw("EXEC InsertLoanApplication @BorrowerName, @PhoneNumber, @BVNNumber, @LoanAmount, @LoanTenure, @LoanRepaymentType, @Interest, @frequency, @LoanApplicationId OUTPUT, @retVal OUTPUT, @retMsg OUTPUT",
                    borrowerNameParam, phoneNumberParam, bvnNumberParam, loanAmountParam, loanTenureParam, loanRepaymentTypeParam, interestParam, frequencyParam, loanApplicationIdParam, retValParam, retMsgParam);

                return new CreateLoanApplicationData
                {
                    code = retValParam.Value.ToString(),
                    message = retMsgParam.Value.ToString(),
                    LoanApplicationid = loanApplicationIdParam.Value.ToString()
                };
            }
            catch (Exception ex)
            {
                return new CreateLoanApplicationData
                {
                    code = "11",
                    message = "Operation not successful: " + ex.Message,
                    LoanApplicationid = null
                };
            }
        }



        public GetAllLoanApplicationData GetAllLoanApplication()
        {
            //Creates an instance of the  GetAllLoanApplicationData     object
            GetAllLoanApplicationData gtloanappdata = new GetAllLoanApplicationData();
            try
            {
              
                var loanappindb = _context.tbl_LoanApplication.ToList();

                //Checks if the response not is null(contains an object/list of objects of type tbl_loanapplication)
                //If its not null it proceeds with the contents of the if block.
                if (loanappindb != null)
                {
                   
                    gtloanappdata.code = "00";
                    gtloanappdata.message = "LoanApplications Retreival Successful";
                    gtloanappdata.loanApplications = loanappindb;
                }
                //If its null (does not contain an object/list of objects of type tbl_Loanapplication)it proceeds with the contents of the else block.
                else
                {
                    gtloanappdata.code = "-11";
                    gtloanappdata.message = "No Loan Application Exist to be retrieved";
                }
            }
            //The catch block handles any errors that may occurr (System/Database)
            catch (Exception ex)
            {
                
                gtloanappdata.code = "-1000";
                gtloanappdata.message = "An Error occurred while performing this action";
            }
            //Returns the result of the operation(containing code, message, issusccesful and an object/list of objects of type tbl_LoanAPplication)
            return gtloanappdata;
        }

        public GetLoanApplicationData GetLoanApplication(string LoanApplicationId)
        {

            //Creates an instance of the GetLoanApplicationData object
            GetLoanApplicationData gtloandata = new GetLoanApplicationData();
            try
            {
                //This is a database operation to find the entity/entites that match the criteria supplied (LoanApplicationID and Flag being 1).
                var loanappindb = _context.tbl_LoanApplication.Where(s => s.LoanApplicationId == LoanApplicationId && s.Flag == 1).FirstOrDefault();

                //Checks if the response not is null(contains an object/list of objects of type tbl_branch)
                //If its not null it proceeds with the contents of the if block.
                if (loanappindb != null)
                {
                  
                    gtloandata.code = "00";
                    gtloandata.message = "LoanApplication Retrieval successful";
                    gtloandata.loanApplication = loanappindb;
                }
                //If its null(does not contain an object/list of objects of type tbl_LoanAPplication) it proceeds with the contents of the else block.
                else
                {
                   
                    gtloandata.code = "-11";
                    gtloandata.message = "LoanApplication Doesn't exist or has been closed";
                }
            }
            //The catch block handles any errors that may occurr (System/Database)
            catch (Exception ex)
            {
               
                gtloandata.code = "-1000";
                gtloandata.message = "An Error occurred performing this action";
            }
            //Returns the result of the operation(containing code, message, issusccesful and an object/list of objects of type tbl_Education)
            return gtloandata;
        }



    }
    }


    
