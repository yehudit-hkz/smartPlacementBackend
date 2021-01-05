using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Web.Configuration;
using System.Configuration;
using System.Security.Principal;

namespace placementDepartmentWebAPI.util
{
    public static class Token
    {
        public static string GenerateToken(string userId )
        {
            string mySecret = ConfigurationManager.AppSettings["jwtsecret"];
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, userId),
                        new Claim(ClaimTypes.Name, userId),
                }),
                Expires = DateTime.UtcNow.AddHours(7),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public static IPrincipal ValidateCurrentToken(string token)
        {
            string mySecret = ConfigurationManager.AppSettings["jwtsecret"];
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claimsPrincipal;
            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch(Exception)
            {
                return null;
            }
            return claimsPrincipal;
        }
    }
}