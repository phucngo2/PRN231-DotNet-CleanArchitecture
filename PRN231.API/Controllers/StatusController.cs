using Microsoft.AspNetCore.Mvc;

namespace PRN231.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello, 世界!");
    }
}
