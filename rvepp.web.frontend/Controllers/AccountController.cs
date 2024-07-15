using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using rvepp.web.frontend.Configuration;
using rvepp.web.frontend.Common;
using rvepp.web.frontend.Objects.JSON;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace rvepp.web.frontend.Controllers
{
    [Route("/api/account")]
    public class AccountController(ApiConfiguration apiConfiguration) : ControllerBase
    {
        private string GenerateToken(string hashToken)
        {
            var iat = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, apiConfiguration.JWTSubject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, iat.ToString()),
                new Claim("Token", hashToken)
            };

            var token = new JwtSecurityToken(
                apiConfiguration.JWTIssuer,
                apiConfiguration.JWTAudience,
                claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiConfiguration.JWTSecret)),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public ActionResult<string> Login(UserLoginRequestItem userLogin)
        {
            var hashToken = (userLogin.UserName + userLogin.Password).ToSha256();

            if (!string.Equals(hashToken, apiConfiguration.JWTHashToken, StringComparison.InvariantCultureIgnoreCase))
            {
                return Forbid();
            }

            return GenerateToken(hashToken);
        }
    }
}