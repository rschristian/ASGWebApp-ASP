using System.Threading.Tasks;
using Domain;
using Domain.AccountViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.Auth;

namespace Web.Api.v1
{
    [Route("/api/v1/account")]
    public class AccountApiController : RootApiController
    {
        private readonly AccountService _accountService;
        
        private readonly IUserIdentity _user;

        public AccountApiController(AccountService accountService, IUserIdentity user)
        {
            _accountService = accountService;
            _user = user;
        }

        [HttpGet("session")]
        public IActionResult GetSession()
        {
            return Json(new SessionData
            {
                User = _user
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (_user.IsAuthenticated)
                return Json(new { Success = false, Response = LoginResponse.AlreadyLoggedIn});

            var result = await _accountService.Login(loginRequest.Email, loginRequest.Password);

            if (result.LoginResponse != LoginResponse.Successful)
                return Json(new {Success = false, Response = result.LoginResponse, result.User?.Email});

            Response.WithCredentials(result.User);
            return Json(new {Success = true});
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.DeleteCredentials();
            return Json(new {Success = true});
        }
    }

    public class SessionData
    {
        public IUserIdentity User { get; set; }
    }
}