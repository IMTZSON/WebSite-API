using Microsoft.AspNetCore.Mvc;
using TEST_API.Databases;
using TEST_API.Helpers.API;

namespace TEST_API.Controllers
{
    [Route("api/v1/no-protected")]
    [ApiController]
    public class NoProtectedController : ControllerBase
    {
        [HttpGet("db")]
        public JsonResult Db()
        {
            var db = new FakeDatabase().GetDb();

            return ApiReponseHelpers.ResponseBase(StatusCodes.Status200OK, db);
        }
    }
}
