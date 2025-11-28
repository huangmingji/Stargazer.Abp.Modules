using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;
using Stargazer.Common.Extend;

namespace Stargazer.Abp.Authentication.Application;

[RemoteService(IsEnabled = false)]
public class TokenGenerator(IConfiguration configuration) : ITransientDependency
{
    private string SecurityKey => configuration?.GetSection("Authentication:SecurityKey").Value ?? "XFEhcc3eNjP9kJrTaokYCQOpQ4SiABBML6QjNKr7EUyiUGGi0Id7uq4LKDLW9Nss";
    private string Issuer => configuration?.GetSection("Authentication:Issuer").Value ?? "Xv1H7WUy";
    private string Audience => configuration?.GetSection("Authentication:Audience").Value ?? "tNzlwPDx";

    public async Task<TokenValidationResult> ValidateToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.FromMinutes(1),
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecurityKey))
        };

        try
        {
            var tokenValidationResult = await new JsonWebTokenHandler().ValidateTokenAsync(token, tokenValidationParameters);
            if (!tokenValidationResult.IsValid)
            {
                // Handle each exception which tokenValidationResult can contain as appropriate for your service
                // Your service might need to respond with a http response instead of an exception.
                if (tokenValidationResult.Exception != null)
                    throw tokenValidationResult.Exception;

                throw new AbpAuthorizationException();
            }

            return tokenValidationResult;
        }
        catch (SecurityTokenExpiredException ex)
        {
            throw new AbpAuthorizationException();
        }
    }

    public string GenerateToken(string userId, Guid? tenantId, List<string> permissions, DateTime expires)
    {
        Dictionary<string, string> claims = new Dictionary<string, string>{{AbpClaimTypes.UserId, userId}};

        if (tenantId != null)
        {
            claims.Add(AbpClaimTypes.TenantId, tenantId.ToString()!);
        }

        claims.Add("permissions", permissions.SerializeObject());
        return GenerateToken(claims, expires);
    }

    public string GenerateToken(string userId, DateTime expires)
    {
        Dictionary<string, string> claims = new Dictionary<string, string> {{AbpClaimTypes.UserId, userId}};
        return GenerateToken(claims, expires);
    }

    public string GenerateToken(Dictionary<string, string> claims, DateTime expires)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecurityKey));
        List<Claim> claimsList = new List<Claim>();
        foreach (var item in claims)
        {
            claimsList.Add(new Claim(item.Key, item.Value));
        }

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claimsList,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}