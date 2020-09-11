using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api_Core_GrupoForta.Security
{
    public static class TokenGenerator
    {
        public static IConfiguration _config;

        public static string GenerarJSONWebToken(string userInfo)
        {

            var secretKey = Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]); 
            var audienceToken = _config["JWT:Audience"];
            var issuerToken = _config["JWT:Issuer"]; //editor
            var expireTime = _config["JWT:ExipireMinutes"];


            var securityKey = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //var token = new JwtSecurityToken(issuerToken, audienceToken, ca {

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userInfo) });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: credentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);

            return jwtTokenString;
        }
    }
}
