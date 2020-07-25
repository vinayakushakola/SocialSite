using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialSiteBusinessLayer.Interfaces;
using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserBusiness _userBusiness;
        private static bool success = false;
        private static string message;
        private static string token;
        
        private readonly string _login = "Login";

        public UserController(IUserBusiness userBusiness, IConfiguration configuration)
        {
            _userBusiness = userBusiness;
            _configuration = configuration;
        }

        /// <summary>
        /// It is used for User Registration
        /// </summary>
        /// <param name="userDetails">User Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(RegistrationRequest userDetails)
        {
            try
            {
                if (ValidateRegistrationRequest(userDetails))
                {
                    var data = _userBusiness.Registration(userDetails);
                    if (data != null)
                    {
                        success = true;
                        message = "User Account Created Successfully";
                        return Ok(new { success, message, data });
                    }
                    else
                    {
                        message = "Email-ID Already Exists";
                        return NotFound(new { success, message });
                    }
                }
                return BadRequest(new { success, message = "Enter Proper Data" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used for User Login
        /// </summary>
        /// <param name="loginDetails">Login Data</param>
        /// <returns>If Data Found return Ok else </returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginRequest loginDetails)
        {
            try
            {
                if (ValidateLoginRequest(loginDetails))
                {
                    var data = _userBusiness.Login(loginDetails);
                    if (data != null)
                    {
                        token = GenerateToken(data, _login);
                        success = true;
                        message = "User Successfully Logged In";
                        return Ok(new { success, message, data, token });
                    }
                    else
                    {
                        message = "No Account Present with this Email-ID & Password";
                        return NotFound(new { success, message });
                    }
                }
                return BadRequest(new { success, message = "Enter Proper Data" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It Validate the Registration Request 
        /// </summary>
        /// <param name="userDetails">New User Data</param>
        /// <returns>If validation successfull, return true else false</returns>
        private bool ValidateRegistrationRequest(RegistrationRequest userDetails)
        {
            if (userDetails == null || string.IsNullOrWhiteSpace(userDetails.FirstName) ||
                    string.IsNullOrWhiteSpace(userDetails.LastName) || string.IsNullOrWhiteSpace(userDetails.Email) ||
                    string.IsNullOrWhiteSpace(userDetails.Password) || userDetails.FirstName.Length < 3 ||
                    userDetails.LastName.Length < 3 || !userDetails.Email.Contains('@') ||
                    !userDetails.Email.Contains('.') || userDetails.Password.Length < 8)
                return false;
            else
                return true;
        }

        /// <summary>
        /// It Validate The Login Request 
        /// </summary>
        /// <param name="loginDetails">Login Data</param>
        /// <returns>If validation successfull, return true else false</returns>
        private bool ValidateLoginRequest(LoginRequest loginDetails)
        {
            if (loginDetails == null || string.IsNullOrWhiteSpace(loginDetails.Email) ||
                string.IsNullOrWhiteSpace(loginDetails.Password) || !loginDetails.Email.Contains('@') ||
                !loginDetails.Email.Contains('.') || loginDetails.Password.Length < 8)
                return false;
            else
                return true;
        }

        /// <summary>
        /// It Generates Token
        /// </summary>
        /// <param name="userDetails">User Details</param>
        /// <param name="tokenType">Token Type</param>
        /// <returns>It returns token</returns>
        private string GenerateToken(UserResponse userDetails, string tokenType)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("UserID", userDetails.ID.ToString()),
                    new Claim("Email", userDetails.Email.ToString()),
                    new Claim("TokenType", tokenType),
                    new Claim("UserRole", userDetails.UserRole.ToString())
                };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"],
                    claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}