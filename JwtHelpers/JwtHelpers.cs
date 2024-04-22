using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using otpsystemback.Models;

namespace otpsystemback.JwtHelpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserToken userAccounts)
        {
            IEnumerable<Claim> claims = new Claim[] {
                    new Claim(ClaimTypes.Email, userAccounts.Email),
                    new Claim(ClaimTypes.NameIdentifier, userAccounts.Id.ToString()),
                    new Claim(ClaimTypes.Hash, userAccounts.Password),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }

        public static UserToken GetTokenKey(UserToken model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserToken();
                if (model == null) throw new ArgumentException(nameof(model));
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer,
                                                    audience: jwtSettings.ValidAudience,
                                                    claims: GetClaims(model),
                                                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                                                    expires: new DateTimeOffset(expireTime).DateTime,
                                                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.Email = model.Email;
                UserToken.Id = model.Id;
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
