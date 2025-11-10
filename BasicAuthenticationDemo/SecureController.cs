using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

[ApiController]
[Route("api/[controller]")]
public class SecureController
: ControllerBase
{
    [Authorize(Roles = "Developer", AuthenticationSchemes = "BasicAuthentication")]
    [HttpGet("secret-developer")]
    public IActionResult GetSecretDevelepoer()
    {
        var user = HttpContext?.User;
        var userName = user?.Identity?.Name;
        var claims = user?.Claims.Where(c => c.Type == ClaimTypes.Role).Select(d => d.Value);
        var roles = string.Join(",", claims!);

        return Ok($"Hoş geldin {user?.Identity?.Name}, rolün:{roles}");
    }
    
    [Authorize(Roles ="Admin",AuthenticationSchemes = "BasicAuthentication")]
    [HttpGet("secret-admin")]
    public IActionResult GetSecretAdmin()
    {
        var user = HttpContext?.User;
        var userName = user?.Identity?.Name;
        var claims = user?.Claims.Where(c => c.Type == ClaimTypes.Role).Select(d => d.Value);
        var roles = string.Join(",", claims!);

        return Ok($"Hoş geldin {user?.Identity?.Name}, rolün:{roles}");
    }

    [AllowAnonymous]
    [HttpGet("public")]
    public IActionResult GetPublic()
    {
        return Ok("Bu açık bir endpointtir");
    }

}