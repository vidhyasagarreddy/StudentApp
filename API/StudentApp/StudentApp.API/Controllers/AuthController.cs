using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentApp.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/auth/callback" }, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                return BadRequest();
            }

            var claims = authenticateResult.Principal.Claims;
            var identity = new ClaimsIdentity(claims, "cookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("cookie", principal);

            return Redirect("/profile");
        }
    }
}
