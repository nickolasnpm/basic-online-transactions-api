using BasicOnlineTransactions.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using BasicOnlineTransactions.Models;
using BasicOnlineTransactions.Helpers;

namespace BasicOnlineTransactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionControllers: ControllerBase
    {
        private readonly IAuthentication _Iauthentication;
        private readonly IConfiguration _Configuration;

        public TransactionControllers(IConfiguration configuration, IAuthentication authentication)
        {
            _Iauthentication = authentication;
            _Configuration = configuration;
        }

        [HttpPost]
        public ActionResult<ResponseMessage> submitTransaction(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            var dbpass = _Configuration.GetSection("databasePassword").Get<string>();
            Console.WriteLine(dbpass);
            try
            {
                requestMessage.partnerpassword = Encoding.UTF8.GetString(Convert.FromBase64String(requestMessage.partnerpassword));

            }
            catch (Exception e)
            {
                var message = "Password is not valid base64 format";

                responseMessage.result = Helpers.MessageResult.failed;
                responseMessage.errormessage = message;

                return BadRequest(message);
            }

            if (_Iauthentication?.ValidateUser(requestMessage.partnerkey, requestMessage.partnerpassword) == true)
            {
                long amountConfirmation = 0;

                requestMessage.items.ForEach(itemelement =>
                {
                    amountConfirmation = (long)(amountConfirmation + itemelement.qty * itemelement.unitprice);
                });

                // check amount similarity

                if (amountConfirmation != requestMessage.totalamount)
                {
                    responseMessage.result = MessageResult.failed;
                    responseMessage.errormessage = ErrorMessageText.INVALID_TOTAL_AMOUNT;

                    return BadRequest(responseMessage);
                }

                // check validity

                try
                {
                    DateTime.TryParse(requestMessage.Timestamp, out DateTime timeNow);

                    if (timeNow.AddMinutes(-5) < DateTime.Now && timeNow.AddMinutes(5) > DateTime.Now)
                    {
                        responseMessage.result = MessageResult.failed;
                        responseMessage.errormessage = ErrorMessageText.EXPIRED;

                        return BadRequest(responseMessage);
                    }
                }
                catch
                {
                    responseMessage.result = MessageResult.failed;
                    responseMessage.errormessage = ErrorMessageText.INVALID_DATE;

                    return BadRequest(responseMessage);
                }

                // validity approved

                responseMessage.result = MessageResult.succeed;
                responseMessage.errormessage = ErrorMessageText.SUCCESS_OPERATION;

                return Ok(responseMessage);
            }
            else
            {
                // wrong key & password combination

                responseMessage.result = MessageResult.failed;
                responseMessage.errormessage = ErrorMessageText.ACCESS_DENIED;

                return Unauthorized(responseMessage);

            }

        }
    }
}
