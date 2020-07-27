//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Definition of PostRepository Methods
//

using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;
using System.Collections.Generic;

namespace SocialSiteRepositoryLayer.Interfaces
{
    public interface IPostRepository
    {
        List<PostResponse> ListOfPosts(int userID);
        
        PostResponse GetPostByID(int userID, int postID);

        PostResponse UploadImage(int userID, string postPath);

        bool LikePost(int userID, int postID);

        List<UserPostResponse> ListOfLikesOnPost(int userID, int postID);

        bool CommentOnPost(int userID, int postID, CommentRequest commentDetails);
    }
}
