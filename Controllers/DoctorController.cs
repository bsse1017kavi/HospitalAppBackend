using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorRepository _doctorRepository = new DoctorRepository();

        [HttpPost("api/doctor/signIn")]
        public IActionResult SignIn(String username, String password)
        {
            if((_doctorRepository.SignIn(username,password)))
            {
                //HttpContext.Session.SetString("isLoggedIn", "true");

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken
                (
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString});
            }

            //String value = HttpContext.Session.GetString("isLoggedIn");

            //Console.Write(value);

            return Unauthorized();
        }

        [HttpPost("api/doctor/add")]
        public IActionResult Add([FromBody] Doctor doctor)
        {
            if ((HttpContext.Session.GetString("isLoggedIn") == "true") || 1==1)
            {
                var addedDoctor = _doctorRepository.Add(doctor);
                return Ok(addedDoctor);
            }

            return Ok("Sorry, you're not logged in");
            
        }

        [HttpGet("api/doctor/getAll")]
        [Authorize]
        public IActionResult GetAllDoctors()
        {
            //if(HttpContext.Session.GetString("isLoggedIn")=="true")
            //{
            //    return Ok(_doctorRepository.GetAll());
            //}

            //return Ok("Sorry, you're not logged in");

            return Ok(_doctorRepository.GetAll());
        }

        [HttpGet("api/doctor/getById")]
        public IActionResult GetById(int doctorId)
        {
            if (HttpContext.Session.GetString("isLoggedIn") == "true")
            {
                var doctor = _doctorRepository.GetById(doctorId);
                return Ok(doctor);
            }

            return Ok("Sorry, you're not logged in");
        }

        [HttpPost("api/doctor/update")]
        public IActionResult UpdateDoctor([FromBody] Doctor doctor)
        {
            if (HttpContext.Session.GetString("isLoggedIn") == "true")
            {
                return Ok(_doctorRepository.Update(doctor));
            }

            return Ok("Sorry, you're not logged in");
        }

        [HttpGet("api/doctor/delete")]
        public IActionResult DeleteDoctor(int doctorId)
        {
            if (HttpContext.Session.GetString("isLoggedIn") == "true")
            {
                var doctor = _doctorRepository.GetById(doctorId);
                _doctorRepository.Delete(doctor);
                return Ok();
            }

            return Ok("Sorry, you're not logged in");
        }

        [HttpPost("api/doctor/signOut")]
        public IActionResult SignOut()
        {
            if (HttpContext.Session.GetString("isLoggedIn") == "true")
            {
                HttpContext.Session.SetString("isLoggedIn", "false");
                return Ok("Successfully Logged out");
            }

            return Ok("Sorry, you're not logged in");
        }

    }
}
