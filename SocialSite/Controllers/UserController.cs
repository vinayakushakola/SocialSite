using Microsoft.AspNetCore.Mvc;
using SocialSiteBusinessLayer.Interfaces;
using SocialSiteCommonLayer.RequestModels;
using System;

namespace SocialSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private static bool success = false;
        private static string message;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
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
                        success = true;
                        message = "User Successfully Logged In";
                        return Ok(new { success, message, data });
                    }
                    else
                    {
                        message = "No Data Found";
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
    }
}