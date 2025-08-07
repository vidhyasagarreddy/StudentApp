using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace StudentApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> Get()
        {
            // In a real application, you would fetch the user from a database
            // based on the authenticated user's identity.
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            return Ok(user);
        }
    }
}
