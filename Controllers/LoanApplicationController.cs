using LoanApplication.DAL.Models;
using LoanApplication.DAL.Models.LoanProducts.RequestObj;
using LoanApplication.DAL.Models.LoanProducts.ResponseObj;
using LoanApplication.DAL.Models.Occurence;
using LoanApplication.DAL.Models.Occurence.RequestObj;
using LoanApplication.DAL.Models.Occurence.ResponseObj;
using LoanApplication.DAL.Repo;
using LoanApplication.DAL.RequestObj;
using LoanApplication.DAL.ResponseObj;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LoanApplication.Controllers
{
    public class LoanApplicationController : Controller
    {
        private readonly IConfiguration _config;
        public LoanOriginationContext _context;
        private readonly ILogger<LoanApplicationController> _logger;

        public LoanApplicationController(IConfiguration config, LoanOriginationContext context, ILogger<LoanApplicationController> logger)
        {
            _config = config;
            _context = context;
            _logger = logger;

        }

        [Route("Loan/LoanCreation")]
        [HttpPost]
        public CreateLoanApplicationData CreateLoanApplication([FromBody] CreateLoanApplicationParam param)
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
                var loanproductcodeParam = new SqlParameter("@LoanProductCode", param.LoanProductCode);
                var loanpurposeParam = new SqlParameter("@LoanPurpose", param.LoanPurpose);

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

                _context.Database.ExecuteSqlRaw("EXEC InsertLoanApplication @BorrowerName, @PhoneNumber, @BVNNumber, @LoanAmount, @LoanTenure, @LoanRepaymentType, @Interest, @frequency,LoanProductCode, @LoanPurpose , @LoanApplicationId OUTPUT, @retVal OUTPUT, @retMsg OUTPUT",
                    borrowerNameParam, phoneNumberParam, bvnNumberParam, loanAmountParam, loanTenureParam, loanRepaymentTypeParam, interestParam, frequencyParam, loanproductcodeParam, loanpurposeParam, loanApplicationIdParam, retValParam, retMsgParam);

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






        [Route("GetAllLoanApplication")]
        [HttpGet]

        public GetAllLoanApplicationResponseData GetAllLoanApplication()
        {
            var repo = new LoanApplicationRepo(_context, _config);
            GetAllLoanApplicationData getAllLoan = repo.GetAllLoanApplication();
            return new GetAllLoanApplicationResponseData { Responsecode = getAllLoan.code, Responsedescription = getAllLoan.message, applications = getAllLoan.loanApplications };
        }

        [Route("GetLoanApplication")]
        [HttpGet]

        public GetLoanApplicationResponse GetLoanApplicationbyloanapplicationid(string LoanApplicationId)
        {
            var repo = new LoanApplicationRepo(_context, _config);
            GetLoanApplicationData gtloandata = repo.GetLoanApplication(LoanApplicationId);
            return new GetLoanApplicationResponse { Responsecode = gtloandata.code, ResponseDescription = gtloandata.message, loanApplication = gtloandata.loanApplication };
        }



        [HttpGet("Occurence/GetAllOccurence")]
        [Produces("application/json")]
        public ActionResult<GetAllOccurenceResponse> GetAllOccurence()

        {


            GetAllOccurenceResponse occurenceResponse = new GetAllOccurenceResponse();
            GetAllOccurenceData gtedata = new GetAllOccurenceData();

            using (var db = new LoanOriginationContext())
            {
                //Creates an instance of the GetAllOccurenceData     object

                try
                {
                    //This is a database operation to find the entity/entites that match the criteria supplied (status being 1).
                    var occurenceindb = db.tbl_ocuurence
                .Where(s => s.Flag == 1).OrderBy(p => p.occurencecode)
                .Select(p => new OccurenceModel { occurencecode = p.occurencecode, occurencename = p.occurencename })
                .ToList();

                    //Checks if the response not is null(contains an object/list of objects of type tbl_occurence)
                    //If its not null it proceeds with the contents of the if block.
                    if (occurenceindb != null)
                    {
                        gtedata.issuccessful = true;
                        gtedata.code = "00";
                        gtedata.message = "Occurence Retreival Successful";
                        gtedata.occurences = occurenceindb;
                    }
                    //If its null (does not contain an object/list of objects of type tbl_occurence)it proceeds with the contents of the else block.
                    else
                    {
                        gtedata.issuccessful = false;
                        gtedata.code = "-11";
                        gtedata.message = "No Occurence Exist to be retrieved";
                    }
                }
                //The catch block handles any errors that may occurr (System/Database)
                catch (Exception ex)
                {
                    gtedata.issuccessful = false;
                    gtedata.code = "-1000";
                    gtedata.message = "An Error occurred while performing this action";
                }
                //Returns the result of the operation(containing code, message, issusccesful and an object/list of objects of type tbl_occurence)

            }
            return new GetAllOccurenceResponse { ResponseCode = gtedata.code, ResponseDescription = gtedata.message, occurence = gtedata.occurences };

        }



        [HttpGet("LoanProduct/GetLoanProducts")]
        [Produces("application/json")]
        public ActionResult<GetAllLoanProductResponse> GetLoanProducts()

        {


            GetAllLoanProductResponse ProductResponse = new GetAllLoanProductResponse();
            GetAllLoanProductData gtedata = new GetAllLoanProductData();

            using (var db = new LoanOriginationContext())
            {
                //Creates an instance of the GetAllLoanProductData     object

                try
                {
                    //This is a database operation to find the entity/entites that match the criteria supplied (status being 1).
                    var loanproductindb = db.tbl_LionBankLoanProducts
                .Where(s => s.Flag == 1).OrderBy(p => p.ProductName)
                .Select(p => new LoanProductDropdowModel { ProductCode = p.ProductCode, ProductName = p.ProductName })
                .ToList();

                    //Checks if the response not is null(contains an object/list of objects of type tbl_lionbankloanproduct)
                    //If its not null it proceeds with the contents of the if block.
                    if (loanproductindb != null)
                    {
                        gtedata.issuccessful = true;
                        gtedata.code = "00";
                        gtedata.message = "Loan Product Retreival Successful";
                        gtedata.loanProducts = loanproductindb;
                    }
                    //If its null (does not contain an object/list of objects of type tbl_loanproduct)it proceeds with the contents of the else block.
                    else
                    {
                        gtedata.issuccessful = false;
                        gtedata.code = "-11";
                        gtedata.message = "No Loan Product Exist to be retrieved";
                    }
                }
                //The catch block handles any errors that may occurr (System/Database)
                catch (Exception ex)
                {
                    gtedata.issuccessful = false;
                    gtedata.code = "-1000";
                    gtedata.message = "An Error occurred while performing this action";
                }
                //Returns the result of the operation(containing code, message, issusccesful and an object/list of objects of type tbl_lionbankloanproduct)

            }
            return new GetAllLoanProductResponse { ResponseCode = gtedata.code, ResponseDescription = gtedata.message, Products = gtedata.loanProducts };

        }

        [Route("Loan/CreateLoanProduct")]
        [HttpPost]
        public CreateLoanProductData CreateLoanProduct([FromBody] CreateLoanProductParam param)
        {
            try
            {
                var productNameParam = new SqlParameter("@ProductName", param.ProductName);
                var productDescriptionParam = new SqlParameter("@ProductDescription", param.ProductDescription);
                var maxInterestRateParam = new SqlParameter("@MaximumInterestRate", param.MaximumInterestRate);
                var minInterestRateParam = new SqlParameter("@MinimumInterestRate", param.MinimumInterestRate);
                var maxLoanAmountParam = new SqlParameter("@MaximumLoanAmount", param.MaximumLoanAmount);
                var maxLoanTenureParam = new SqlParameter("@MaximumLoanTenure", param.MaximumLoanTenure);

                var productCodeParam = new SqlParameter
                {
                    ParameterName = "@ProductCode",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 3,
                    Direction = ParameterDirection.Output
                };
                var retValParam = new SqlParameter
                {
                    ParameterName = "@RetVal",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                var retMsgParam = new SqlParameter
                {
                    ParameterName = "@RetMsg",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Direction = ParameterDirection.Output
                };

                _context.Database.ExecuteSqlRaw("EXEC CreateLoanProduct @ProductName, @ProductDescription, @MaximumInterestRate, @MinimumInterestRate, @MaximumLoanAmount, @MaximumLoanTenure, @ProductCode OUTPUT, @RetVal OUTPUT, @RetMsg OUTPUT",
                    productNameParam, productDescriptionParam, maxInterestRateParam, minInterestRateParam, maxLoanAmountParam, maxLoanTenureParam, productCodeParam, retValParam, retMsgParam);

                return new CreateLoanProductData
                {
                    code = retValParam.Value.ToString(),
                    message = retMsgParam.Value.ToString(),
                    ProductCode = productCodeParam.Value.ToString()
                };
            }
            catch (Exception ex)
            {
                return new CreateLoanProductData
                {
                    code = "1",
                    message = "Loan Product Not Created: " + ex.Message,
                    ProductCode = null
                };
            }
        }




        [Route("User/CreateUser")]
        [HttpPost]
        public CreateUserResponse CreateUser([FromBody] CreateUserParams param)
        {
            try
            {
                // Convert Base64 image data to bytes
                byte[] profilePictureData = Convert.FromBase64String(param.ProfilePictureBase64);

                // Set up parameters for the stored procedure
                var usernameParam = new SqlParameter("@Username", param.Username);
                var firstNameParam = new SqlParameter("@FirstName", param.FirstName);
                var lastNameParam = new SqlParameter("@LastName", param.LastName);
                var emailParam = new SqlParameter("@Email", param.Email);
                var passwordParam = new SqlParameter("@Password", param.Password);
                var dateCreatedParam = new SqlParameter("@DateCreated", DateTime.UtcNow);

              

                // Hardcoded role value
                var roleParam = new SqlParameter("@Role", "regular");

                // Get client's IP address
                var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                var ipAddressParam = new SqlParameter("@IpAddress", ipAddress);

                var sessionIdParam = new SqlParameter("@SessionId", "Omzy47");

                // Hardcoded password reset code
                var passwordResetCode = "12345"; // Change this to a valid password reset code
                var passwordResetCodeParam = new SqlParameter("@PasswordResetCode", passwordResetCode);

                var profilePictureParam = new SqlParameter("@profile_picture", SqlDbType.VarBinary, -1);
                profilePictureParam.Value = profilePictureData;

                var profilePictureTypeParam = new SqlParameter("@profile_picture_type", SqlDbType.VarChar, 20);
                profilePictureTypeParam.Value = param.ProfilePictureContentType;

                var returnMessageParam = new SqlParameter
                {
                    ParameterName = "@ReturnMessage",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Direction = ParameterDirection.Output
                };
                var returnValueParam = new SqlParameter
                {
                    ParameterName = "@ReturnValue",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                // Execute the stored procedure
                _context.Database.ExecuteSqlRaw("EXEC CreateUser @Username, @FirstName, @LastName, @Email, @Password, @DateCreated, @Role, @IpAddress, @SessionId, @PasswordResetCode, @profile_picture, @profile_picture_type, @ReturnMessage OUTPUT, @ReturnValue OUTPUT",
                    usernameParam, firstNameParam, lastNameParam, emailParam, passwordParam, dateCreatedParam, roleParam, ipAddressParam, sessionIdParam, passwordResetCodeParam, profilePictureParam, profilePictureTypeParam, returnMessageParam, returnValueParam);

                return new CreateUserResponse
                {
                    ReturnMessage = returnMessageParam.Value.ToString(),
                    ReturnValue = Convert.ToInt32(returnValueParam.Value)
                };
            }
            catch (Exception ex)
            {
                return new CreateUserResponse
                {
                    ReturnMessage = "User Not Created: " + ex.Message,
                    ReturnValue = 3 // You can set an appropriate error code here
                };
            }
        }





        [Route("User/Login")]
        [HttpPost]
        public UserLoginResponseModel Login([FromBody] UserLoginModel loginModel)
        {
            try
            {
                var usernameParam = new SqlParameter("@Username", loginModel.Username);
                var passwordParam = new SqlParameter("@Password", loginModel.Password);

                var isVerifiedParam = new SqlParameter
                {
                    ParameterName = "@IsVerified",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };
                var userIdParam = new SqlParameter
                {
                    ParameterName = "@UserId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                _context.Database.ExecuteSqlRaw("EXEC VerifyUserLogin @Username, @Password, @IsVerified OUTPUT, @UserId OUTPUT",
                    usernameParam, passwordParam, isVerifiedParam, userIdParam);

                var responseModel = new UserLoginResponseModel
                {
                    IsVerified = Convert.ToBoolean(isVerifiedParam.Value),
                    UserId = Convert.ToInt32(userIdParam.Value)
                };

                return responseModel;
            }
            catch (Exception ex)
            {
                // You can handle exceptions here and create an appropriate response
                return new UserLoginResponseModel
                {
                    IsVerified = false,
                    UserId = -1 // Invalid user ID
                };
            }
        }


    }
}





