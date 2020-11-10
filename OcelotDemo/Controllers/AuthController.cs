using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OcelotDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;

        public AuthController(IJWTAuthenticationManager jWTAuthenticationManager)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        [HttpGet]
        public IEnumerable Get()
        {
            return new string[] { "New York", "New Jersey" };
        }

        // GET: api/<AuthController>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginDetails login)
        {
            var token = jWTAuthenticationManager.Authenticate(login.UserName, login.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }       
    }
}
