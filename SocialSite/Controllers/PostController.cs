using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialSiteBusinessLayer.Interfaces;
using SocialSiteCommonLayer.RequestModels;

namespace SocialSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostBusiness _postBusiness;
        private readonly IConfiguration _configuration;
        private static bool success = false;
        private static string message;

        public PostController(IPostBusiness postBusiness, IConfiguration configuration)
        {
            _postBusiness = postBusiness;
            _configuration = configuration;
        }

        /// <summary>
        /// Shows List of Images
        /// </summary>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpGet]
        public IActionResult ListOfPosts()
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
                        var data = _postBusiness.ListOfPosts(userID);
                        if (data != null)
                        {
                            success = true;
                            message = "List of Images Fetched Successfully";
                            return Ok(new { success, message, data });
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
        /// Show Image by ID
        /// </summary>
        /// <param name="postID">Post-ID</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpGet]
        [Route("{postID}")]
        public IActionResult GetPostByPostID(int postID)
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
                        var data = _postBusiness.GetPostByID(userID, postID);
                        if (data != null)
                        {
                            success = true;
                            message = "Image Fetched Successfully";
                            return Ok(new { success, message, data });
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
        /// Upload
        /// </summary>
        /// <param name="formFile">Image Path</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        public IActionResult UploadImage(IFormFile formFile)
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
                        var data = _postBusiness.UploadImage(userID, postPath);
                        if (data != null)
                        {
                            success = true;
                            message = "Post Uploaded Successfully";
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
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Like Post
        /// </summary>
        /// <param name="postID">Post-ID</param>
        /// <returns>If Data Liked return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        [Route("{postID}/Like")]
        public IActionResult LikeImage(int postID)
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
                        var data = _postBusiness.LikePost(userID, postID);
                        if (data)
                        {
                            success = true;
                            message = "Post Liked Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "Post Not Found";
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
        /// Comment on Post
        /// </summary>
        /// <param name="postID">Post-ID</param>
        /// <param name="commentDetails">Comment Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        [Route("{postID}/Comment")]
        public IActionResult CommentOnImage(int postID, CommentRequest commentDetails)
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
                        var data = _postBusiness.CommentOnPost(userID, postID, commentDetails);
                        if (data)
                        {
                            success = true;
                            message = "Comment on Post is Successfull";
                            return Ok(new { success, message });
                        }
                        else
                        {
                            message = "Post Not Found";
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