using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestASPNET.Business;
using RestASPNET.Data.VO;

namespace RestASPNET.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [Authorize("Bearer")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null)
                BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(user);

            if (token == null)
                Unauthorized();

            return Ok();
        }
    }
}
