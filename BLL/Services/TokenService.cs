using BLL.DTO;
using BLL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
    public class TokenService: ITokenService
    {
        public object Token(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return response;
        }
        public ClaimsIdentity GetIdentity(string login, string password)
        {
            Person person = people.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        #region Настройки и тестовые данные
        /// <summary>
        /// Настройки
        /// </summary>
        public class AuthOptions
        {
            public const string ISSUER = "MyAuthServer";
            public const string AUDIENCE = "MyAuthClient";
            const string KEY = "mysupersecret_secretkey!123";
            public const int LIFETIME = 180;
            public static SymmetricSecurityKey GetSymmetricSecurityKey()
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
            }
        }
        /// <summary>
        /// Тестовые данные
        /// </summary>
        private List<Person> people = new List<Person>
        {
            new Person {Login="adminkaspi@gmail.com", Password="12345", Role = "admin" },
            new Person { Login="userkaspi@gmail.com", Password="12345", Role = "user" }
        };
        #endregion


    }
}
