#if DEBUG
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ErrorTestController : ControllerBase
{
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }
    
    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        return BadRequest("Bad Request");
    }
    
    [HttpGet("not-found")]
    public IActionResult GetNotFound()
    {
        return  NotFound();
    }
    
    [HttpGet("internal-error")]
    public IActionResult  GetInternalError()
    { 
        throw new Exception("Internal error");
    }
    
    [HttpPost("validation-error")]
    public IActionResult GetValidationError(Task task)
    {
        return Ok();
    }
}
#endif
