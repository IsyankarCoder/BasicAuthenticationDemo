using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

public class BasicAuthHandler(
  IOptionsMonitor<AuthenticationSchemeOptions> options,
  ILoggerFactory logger,
  UrlEncoder encoder
   ) :
  AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
  {
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing Authentication Header"));
        }

        try
        {
            var reqHeader = Request.Headers["Authorization"];
            var authHeader = AuthenticationHeaderValue.Parse(reqHeader!);

            var credentialsByte = Convert.FromBase64String(authHeader.Parameter ?? "");
            var credentials = Encoding.UTF8.GetString(credentialsByte).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            if (username == "admin" && password == "pas123")
            {
                var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));

            }
            else
                return Task.FromResult(AuthenticateResult.Fail("Invalid UserName or PassWord"));
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Autherization Handler"));
        }

    }
}



