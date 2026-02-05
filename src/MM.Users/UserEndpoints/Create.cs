using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using FastEndpoints;

namespace MM.Users.UserEndpoints;

internal class Create : Endpoint<CreateUserRequest>
{
  private readonly UserManager<ApplicationUser> _userManager;

  public Create(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }
  public override void Configure()
  {
    Post("/users");
    AllowAnonymous();
  }
  public override async Task HandleAsync(CreateUserRequest req, CancellationToken cancellationToken)
  {
    var newUSer = new ApplicationUser
    {
      Email =  req.Email,
      UserName = req.Email,
    };

    await _userManager.CreateAsync(newUSer, req.Password);

    await Send.OkAsync();

  }
}
