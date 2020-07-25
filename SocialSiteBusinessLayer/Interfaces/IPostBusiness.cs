//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Definition of PostBusiness Methods
//

using SocialSiteCommonLayer.ResponseModels;
using System.Collections.Generic;
namespace SocialSiteBusinessLayer.Interfaces
{
    public interface IPostBusiness
    {
        List<PostResponse> ListOfPosts(int userID);

        PostResponse GetPostByID(int userID, int postID);

        PostResponse UploadImage(int userID, string postPath);

        bool LikePost(int userID, int postID);
    }
}
