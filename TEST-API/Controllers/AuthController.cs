using Microsoft.AspNetCore.Mvc;
using TEST_API.Helpers.API;
using TEST_API.Helpers.JWT;
using TEST_API.Models.JWT;

namespace TEST_API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public JsonResult Login([FromBody] AuthRequest authRequest)
        {
            var jwtAuthenticationManager = new JwtAuthManagerHelpers();

            var authResult = jwtAuthenticationManager.Authenticate(
                authRequest.UserName,
                authRequest.Password);

            if (authResult != null)
            {
                return ApiReponseHelpers.ResponseBase(StatusCodes.Status200OK, authResult);
            }
            else
            {
                return ApiReponseHelpers.ResponseErrorApi(StatusCodes.Status401Unauthorized, "Unauthorized");
            }
        }
    }
}
