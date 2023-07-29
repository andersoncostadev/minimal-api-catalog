using Microsoft.AspNetCore.Authorization;
using MinimalAPICatalogo.Domain;
using MinimalAPICatalogo.Services;

namespace MinimalAPICatalogo.ApiEndpoints
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoint(this WebApplication application)
        {
            application.MapPost("/login", [AllowAnonymous] (UserModel user, ITokenService token) =>
            {
                if (user == null) return Results.BadRequest("Login Inválido");

                if (user.Name == "andersoncosta" && user.Password == "numsey#123")
                {
                    var tokenString = token.GenerateToken(
                        application.Configuration["Jwt:Key"]!,
                        application.Configuration["Jwt:Issuer"]!,
                        application.Configuration["Jwt:Audience"]!,
                        user);
                    return Results.Ok(new { token = tokenString });
                }
                else return Results.BadRequest("Login Inválido");
            }).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status200OK).WithName("Login").WithTags("Authentication");
        }
    }
}
