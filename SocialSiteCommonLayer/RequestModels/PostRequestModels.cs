//
// Author  : Vinayak Ushakola
// Date    : 26/07/2020
// Purpose : It Contain Post Request Models
//

using System.ComponentModel.DataAnnotations;

namespace SocialSiteCommonLayer.RequestModels
{
    public class CommentRequest
    {
        [Required]
        public string Comment { get; set; }
    }
}
