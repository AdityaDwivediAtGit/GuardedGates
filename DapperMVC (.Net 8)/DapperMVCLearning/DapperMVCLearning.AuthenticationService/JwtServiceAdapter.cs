using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DapperMVCLearning.AuthenticationService
{
    public class JwtServiceAdapter : JwtService
    {
        public JwtServiceAdapter(string secretKey, string issuer, string audience) : base(secretKey, issuer, audience)
        {
        }
        #region constructor injection
        ////private readonly string _secretKey;
        ////private readonly string _issuer;
        ////private readonly string _audience;
        //private readonly JwtService _jwtService;

        ////public JwtServiceAdapter(string secretKey, string issuer, string audience)
        //public JwtServiceAdapter(JwtService jwtService) : base(jwtService._secretKey, jwtService._issuer, jwtService._audience)
        //    {
        //    _jwtService = jwtService;
        //    //_jwtService = new JwtService(secretKey, issuer, audience);
        //}
        #endregion


        public override bool ValidateToken(string token)
        {
            try
            {
                var principal = GetPrincipal(token);
                return principal != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing payment: {ex.Message}");
                throw;
            }
            //return principal != null;
        }
    }
}
