//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain User Request Models
//

using System.ComponentModel.DataAnnotations;

namespace SocialSiteCommonLayer.RequestModels
{
    /// <summary>
    /// Registration Details
    /// </summary>
    public class RegistrationRequest
    {
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Your FirstName should only contain Alphabets!")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Your LastName should only contain Alphabets!")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please Enter a Valid Email-ID")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Your Password Should be Minimum Length of 8")]
        public string Password { get; set; }
    }

    /// <summary>
    /// Login Details
    /// </summary>
    public class LoginRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please Enter a Valid Email-ID")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Your Password Should be Minimum Length of 8")]
        public string Password { get; set; }
    }
}
