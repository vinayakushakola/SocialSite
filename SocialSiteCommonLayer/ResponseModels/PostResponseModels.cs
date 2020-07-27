//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Post Response Models
//

using System;

namespace SocialSiteCommonLayer.ResponseModels
{
    public class PostResponse
    {
        public int PostID { get; set; }

        public int UserID { get; set; }

        public string PostPath { get; set; }

        public int PostLikes { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }

    public class UserLikeResponse
    {
        public int UserID { get; set; }
        public string Name { get; set; }
    }

    public class UserCommentResponse
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
