using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersisentStudentWebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersisentStudentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private StudentDbContext _context;
        private IConfiguration _config;
        public AuthenticationController(StudentDbContext context , IConfiguration config)
        {
            _context = context;
            _config = config;


        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();
            var obj = Authenticate(user);
            if(obj != null)
            {
                var tokenString = GenerateJSONWebToken(obj);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
        User Authenticate(User user)
        {
            var obj = _context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            return obj;
        }
        string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                              _config["Jwt:Issuer"],
                               null,
                               expires: DateTime.Now.AddMinutes(120),
                               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
