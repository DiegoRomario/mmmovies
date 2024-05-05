using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using MMMovies.Users.Domain;
using MMMovies.Users.Endpoints.Requests;

namespace MMMovies.Users.Endpoints;

internal class Login(UserManager<ApplicationUser> userManager) : Endpoint<UserLoginRequest>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserLoginRequest request, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        if (user == null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        var loginSuccessful = await _userManager.CheckPasswordAsync(user,
                          request.Password);

        if (!loginSuccessful)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var jwtSecret = Config["Auth:JwtSecret"]!;
        var token = JwtBearer.CreateToken(o =>
                                            {
                                                o.SigningKey = jwtSecret;
                                                o.ExpireAt = DateTime.UtcNow.AddDays(1);
                                                o.User.Claims.Add(("EmailAddress", user.Email!));
                                            }
                                          );

        await SendAsync(token, cancellation: ct);
    }

}
