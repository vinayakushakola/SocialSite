//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Implementation of IPostBusiness Methods
//

using SocialSiteBusinessLayer.Interfaces;
using SocialSiteCommonLayer.ResponseModels;
using SocialSiteRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SocialSiteBusinessLayer.Services
{
    public class PostBusiness : IPostBusiness
    {
        private readonly IPostRepository _postRepository;

        public PostBusiness(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public List<PostResponse> ListOfPosts(int userID)
        {
            if (userID > 0)
                return _postRepository.ListOfPosts(userID);
            else
                return null;
        }

        public PostResponse GetPostByID(int userID, int postID)
        {
            if (userID > 0 && postID > 0)
                return _postRepository.GetPostByID(userID, postID);
            else
                return null;
        }


        public PostResponse UploadImage(int userID, string postPath)
        {
            if (userID > 0 && postPath != null)
                return _postRepository.UploadImage(userID, postPath);
            else
                return null;
        }

        public bool LikePost(int userID, int postID)
        {
            if (userID > 0 && postID > 0)
                return _postRepository.LikePost(userID, postID);
            else
                return false;
        }
    }
}
