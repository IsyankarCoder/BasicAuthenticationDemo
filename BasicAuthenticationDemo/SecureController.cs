using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

[ApiController]
[Route("api/[controller]")]
public class SecureController
: ControllerBase
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [HttpGet("secret")]
    public IActionResult GetSecret()
    {
        return Ok("You have accessed a protection endpoint");
    }

    [AllowAnonymous]
    [HttpGet("public")]
    public IActionResult GetPublic()
    {
        return Ok("Bu açık bir endpointtir");
    }

}