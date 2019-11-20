using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace dotnetstarter {
    /// <summary>
    /// A simple helper class to abstract the JWT Auth configuration out of startup.cs
    /// </summary>
    public static class JwtSecurityHelper {
        public static void UseJwtAuth(this IServiceCollection services, string securityKey) {
            byte[] secureKey = Encoding.ASCII.GetBytes(securityKey);

            AuthenticationBuilder builder = services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            builder.AddJwtBearer(options => {
                options.Events = GetJwtBearerEvents();
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = GetTokenValidationParameters(secureKey);
            });
        }

        public static TokenValidationParameters GetTokenValidationParameters(byte[] securityKey) {
            return new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        public static JwtBearerEvents GetJwtBearerEvents() {
            var result = new JwtBearerEvents {OnTokenValidated = OnTokenValidated};
            return result;
        }

        /// <summary>
        /// Fill this out with any code required to get a user from the database and match them with a particular JWT.
        /// This will allow Dep Injection of the user and their related information into the request layer.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Task OnTokenValidated(TokenValidatedContext context) {
            // -- TODO
            //var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            //var userId = context.Principal.Identity.Name;
            //var user = userService.GetById(userId);

            //if (user == null) {
            //    context.Fail("Unauthorized");
            //}
            return Task.CompletedTask;
        }

        public static string GenerateJwtToken(string authKey) {
            byte[] key = Encoding.ASCII.GetBytes(authKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("some-claim", "some-value")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
