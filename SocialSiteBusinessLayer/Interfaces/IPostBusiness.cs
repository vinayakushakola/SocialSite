﻿//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Definition of PostBusiness Methods
//

using SocialSiteCommonLayer.RequestModels;
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

        List<UserLikeResponse> ListOfLikesOnPost(int userID, int postID);

        bool CommentOnPost(int userID, int postID, CommentRequest commentDetails);

        List<UserCommentResponse> ListOfCommentsOnPost(int userID, int postID);
    }
}
