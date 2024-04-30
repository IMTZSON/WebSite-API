using Microsoft.AspNetCore.Mvc;
using TEST_API.Models.API;

namespace TEST_API.Helpers.API
{
    public class ApiReponseHelpers
    {
        public static JsonResult ResponseErrorApi(int code, string descrption)
        {
            return new JsonResult(new ErrorApiReponse
            {
                Message = ServerResponseHelpers.GetErrorMessage(code),
                Description = descrption
            })
            { StatusCode = code };
        }

        public static JsonResult ResponseBase(int code, object? data = null)
        {
            return new JsonResult(data) { StatusCode = code };
        }
    }
}
