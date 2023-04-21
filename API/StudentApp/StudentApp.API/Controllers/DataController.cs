using Microsoft.AspNetCore.Mvc;

namespace StudentApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Student>> Get()
        {
            var students = new List<Student>
            {
                new Student
                {
                    Name = "Alice",
                    Age =  20,
                    Hobbies = new[] { "reading", "swimming", "coding" }
                },
                new Student
                {
                    Name = "Bob",
                    Age =  22,
                    Hobbies = new[] { "painting", "dancing", "singing" }
                }
            };

            return Ok(students);
        }
    }
}