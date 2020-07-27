using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialSiteBusinessLayer.Interfaces;
using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        /// Shows All the Users
        /// </summary>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpGet]
        public IActionResult ListOfUsers()
        {
            try
            {
                var data = _userBusiness.ListOfUsers();
                if (data != null)
                {
                    success = true;
                    message = "Users Data Fetched Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Users Found";
                    return NotFound(new { success, message });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
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
        /// Upload Profile Picture
        /// </summary>
        /// <param name="formFile">Image Path</param>
        /// <returns>If Image Uploaded</returns>
        [HttpPost]
        [Route("ProfilePhoto")]
        public IActionResult UploadProfileImage(IFormFile formFile)
        {
            try
            {
                var postPath = UploadImageToCloudinary(formFile);
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "Login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = _userBusiness.UploadProfileImage(userID, postPath);
                        if (data != null)
                        {
                            success = true;
                            message = "Profile Picture Uploaded Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "No Data Provided";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Send Friend Request 
        /// </summary>
        /// <param name="friendID">Friend-ID</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        [Route("{friendID}/SendRequest")]
        public IActionResult SendFriendRequest(int friendID)
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "Login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = _userBusiness.SendFriendRequest(userID, friendID);
                        if (data)
                        {
                            success = true;
                            message = "Friend Request Sent Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "No User Present with this ID";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Shows Friend Requests
        /// </summary>
        /// <returns>If Data Found return Ok else Bad Request</returns>
        [HttpGet]
        [Route("FriendRequests")]
        public IActionResult FriendRequests()
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "Login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = _userBusiness.FriendRequests(userID);
                        if (data != null)
                        {
                            success = true;
                            message = "Friend Request Received Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "No Friend Requests";
                            return Ok(new { success, message });
                        }
                    }
                }
                message = "Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Accept Friend Request
        /// </summary>
        /// <param name="friendID">Friend-ID</param>
        /// <returns>If Request Accepted return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        [Route("{friendID}/AcceptRequest")]
        public IActionResult AcceptFriendRequest(int friendID)
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "Login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = _userBusiness.AcceptFriendRequest(userID, friendID);
                        if (data)
                        {
                            success = true;
                            message = "Friend Request Accepted Successfully";
                            return Ok(new { success, message });
                        }
                        else
                        {
                            message = "No Data Found";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Reject Friend Request
        /// </summary>
        /// <param name="friendID">Friend-ID</param>
        /// <returns>If Request Rejected return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        [Route("{friendID}/RejectRequest")]
        public IActionResult RejectFriendRequest(int friendID)
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "Login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = _userBusiness.AcceptFriendRequest(userID, friendID);
                        if (data)
                        {
                            success = true;
                            message = "Friend Request Rejected Successfully";
                            return Ok(new { success, message });
                        }
                        else
                        {
                            message = "No Data Found";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "Token Invalid!";
                return BadRequest(new { success, message });
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

        /// <summary>
        /// It is used to Upload image into the Cloudinary
        /// </summary>
        /// <param name="formFile">Image Path</param>
        /// <returns>If Image Uploaded Successfully return Image Link else Exception</returns>
        private string UploadImageToCloudinary(IFormFile formFile)
        {
            try
            {
                var myAccount = new Account(_configuration["Cloudinary:CloudName"], _configuration["Cloudinary:ApiKey"], _configuration["Cloudinary:ApiSecret"]);

                Cloudinary _cloudinary = new Cloudinary(myAccount);

                var imageUpload = new ImageUploadParams
                {
                    File = new FileDescription(formFile.FileName, formFile.OpenReadStream()),
                };

                var uploadResult = _cloudinary.Upload(imageUpload);

                return uploadResult.SecureUrl.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}