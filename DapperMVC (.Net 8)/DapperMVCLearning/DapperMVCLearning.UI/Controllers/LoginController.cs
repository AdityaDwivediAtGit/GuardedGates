using Dapper;
using DapperMVCLearning.AuthenticationService;
using DapperMVCLearning.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
//using DapperMVCLearning.AuthenticationService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DapperMVCLearning.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        #region dependency injection _config
        private readonly IConfiguration _config;
        //private readonly JwtService _jwt;
        public LoginController(IConfiguration config) //JwtService jwt 
        { 
            _config = config;
            //_jwt = jwt;
        }
        #endregion


        // Plan:
        // get request will not be entertained
        // post request will check for bearer token
        [HttpPost]
        [AllowAnonymous]
        public IActionResult login_method([FromBody] LoginModel model)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("SqlServerConnection")))
            {
                connection.Open();

                // Query the database for the user
                var query = "SELECT [Id], [Username], [Email], [passwd] FROM [Users] WHERE [Username] = @Username AND [Email] = @Email AND [passwd] = @Passwd";
                var user = connection.QueryFirstOrDefault<LoginModel>(query, new { model.Username, model.Email, model.passwd });

                if (user == null)
                {
                    // User not found or credentials do not match
                    return Unauthorized();
                }
                var jwtService = new JwtService(_config["Jwt:SecretKey"], _config["Jwt:Issuer"], _config["Jwt:Audience"]);
                var token = jwtService.GenerateToken(model.Username);

                return Ok(new { Token = token });
            }
            //return BadRequest();
        }




        // GET: api/<LoginController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<LoginController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<LoginController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<LoginController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<LoginController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
