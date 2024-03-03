using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DapperMVCLearning.AuthenticationService
{
    public class JwtService
    {
        #region Dependency injection
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService(string secretKey, string issuer, string audience)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
        }
        #endregion

        #region Generator
        public string GenerateToken(string username, int expireMinutes = 60)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken (
                                                issuer: _issuer, 
                                                audience: _audience, 
                                                claims: new[] {new Claim (ClaimTypes.Name, username)},
                                                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                                                signingCredentials: credentials
                                                ) ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region checker algo
        public ClaimsPrincipal GetPrincipal(string token) 
        {
            try
            {
                var token_handler = new JwtSecurityTokenHandler();
                var jwtToken = token_handler.ReadToken(token) as JwtSecurityToken;

                // if input token null nahi hai, ya algorithm header koi dusri algorithm nahi bata raha hai.
                if (jwtToken != null || jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) {
                    return null;
                }
                var key = Encoding.UTF8.GetBytes(_secretKey);

                var validation_parameters = new TokenValidationParameters { 
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                var principal = token_handler.ValidateToken(token: token, 
                                                            validation_parameters,
                                                            out _);
                //var principal = token_handler.ValidateToken(token: token,
                //                                            TokenValidationParameters: validation_parameters,
                //                                            out validatedToken: _);

                return principal;
            }
            catch { return null; }
        }
        #endregion

        #region Bool validator
        public virtual bool ValidateToken(string token)
        {
            var principal = GetPrincipal(token);
            return principal != null;
        }
        #endregion

        #region ussage
        //var jwtService = new JwtService("your_secret_key", "your_issuer", "your_audience");

        //// Generate a token
        //var token = jwtService.GenerateToken("username");

        //// Validate a token
        //var isValid = jwtService.ValidateToken(token);

        //// Get claims from a token
        //var principal = jwtService.GetPrincipal(token);
        #endregion
    }
}