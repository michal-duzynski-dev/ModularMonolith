using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MM.Users.UserEndpoints;

internal class Login : Endpoint<LoginUserRequest>
{
  private readonly UserManager<ApplicationUser> _userManager;

  public Login(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }
  public override void Configure()
  {
    Post("/users/login");
    AllowAnonymous();
  }
  public override async Task HandleAsync(LoginUserRequest req, CancellationToken cancellationToken)
  {
    var user = await _userManager.FindByEmailAsync(req.Email);
    if (user == null)
    {
      await Send.UnauthorizedAsync();
      return;
    }

    var success = await _userManager.CheckPasswordAsync(user, req.Password);
    if (!success)
    {
      await Send.UnauthorizedAsync();
      return;
    }

    var jwtSecret = Config["Auth:JwtSecret"]!;
    var token = JwtBearer.CreateToken(o =>
    {
      o.SigningKey = jwtSecret;
      o.User.Claims.Add(("EmailAddress", user.Email!));
    });

    await Send.OkAsync(new { Token = token });


  }
}
