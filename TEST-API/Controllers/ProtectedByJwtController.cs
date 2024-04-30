using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TEST_API.Databases;
using TEST_API.Helpers.API;

namespace TEST_API.Controllers
{
    [Route("api/v1/protected-by-jwt")]
    [ApiController]
    public class ProtectedByJwtController : ControllerBase
    {
        [HttpGet("superadmin-only")]
        [Authorize(Roles = "SUPERADMIN")]
        public JsonResult DbSuperAdmin()
        {
            var db = new FakeDatabase().GetDb();

            return ApiReponseHelpers.ResponseBase(StatusCodes.Status200OK, db);
        }

        [HttpGet("admin-only")]
        [Authorize(Roles = "SUPERADMIN, ADMIN")]
        public JsonResult DbAdmin()
        {
            var logged = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var db = new FakeDatabase().GetDb();

            var res = db.Where(x => x.Id == logged).FirstOrDefault();

            return ApiReponseHelpers.ResponseBase(StatusCodes.Status200OK, res);
        }

        [HttpGet("operator-only")]
        [Authorize(Roles = "OPERATOR")]
        public JsonResult DbOperator()
        {
            var logged = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var db = new FakeDatabase().GetDb();

            var res = db.Where(x => x.Id == logged).FirstOrDefault();

            return ApiReponseHelpers.ResponseBase(StatusCodes.Status200OK, res);
        }
    }
}
