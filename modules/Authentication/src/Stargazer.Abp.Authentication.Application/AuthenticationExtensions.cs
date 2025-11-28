using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Stargazer.Abp.Authentication.Application;

public static class AuthenticationExtensions
{
    public static void UseJwtBearerAuthentication(this IServiceCollection services, string[] permissions, string scheme = JwtBearerDefaults.AuthenticationScheme)
    {
        IConfiguration? configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        var securityKey = configuration?.GetSection("Authentication:SecurityKey").Value ?? "XFEhcc3eNjP9kJrTaokYCQOpQ4SiABBML6QjNKr7EUyiUGGi0Id7uq4LKDLW9Nss";
        var issuer = configuration?.GetSection("Authentication:Issuer").Value ?? "Xv1H7WUy";
        var audience = configuration?.GetSection("Authentication:Audience").Value ?? "tNzlwPDx";
            
        services.AddAuthorization(options =>
        {
            foreach (var permission in permissions)
            {
                options.AddPolicy(permission, policy => policy.Requirements.Add(new PermissionRequirement(permission)));
            }
        }).AddAuthentication(options =>
        {                    
            options.DefaultAuthenticateScheme = scheme;
            options.DefaultChallengeScheme = scheme;
        }).AddJwtBearer(scheme,
            jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),//秘钥
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    RequireExpirationTime = true
                };
            }
        );
        services.AddTransient<IAuthorizationHandler, PermissionHandler>();
    }
}