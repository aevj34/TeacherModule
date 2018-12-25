using api.Application.Dto;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace api.Common.Infrastructure.Security
{
    public class JwtTokenProvider
    {

        private String secretKey = "ahbasshfbsahjfbshajbfhjasbfashjbfsajhfvashjfashfbsahfbsahfksdjf";

        public String BuildJwtToken(UserAuthDto userAuthDto)
        {
            var claimsdata = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userAuthDto.name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("shortName", userAuthDto.shortName),
            new Claim("fullName", userAuthDto.fullName),
            new Claim("userID", userAuthDto.id.ToString()),
            new Claim("roleID", userAuthDto.roleID.ToString()),
            new Claim("schoolID", userAuthDto.schoolID.ToString())

        };

            //var claimsdata = new[] { new Claim(ClaimTypes.Name, userAuthDto.name) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
         var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
         var token = new JwtSecurityToken(
         issuer: "mysite.com",
         audience: "mysite.com",
         expires: DateTime.Now.AddMinutes(1),
         claims: claimsdata,
         signingCredentials: signInCred
        );

      var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

      return tokenString;

        }

    }
}
