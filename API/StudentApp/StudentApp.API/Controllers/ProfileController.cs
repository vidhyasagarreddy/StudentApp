using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace StudentApp.Controllers
{
    [ApiController]
    [Route("profile")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        public IActionResult Profile()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(claims);
        }
    }
}
