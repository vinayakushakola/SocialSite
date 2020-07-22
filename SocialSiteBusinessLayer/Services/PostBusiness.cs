//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Implementation of IPostBusiness Methods
//

using SocialSiteBusinessLayer.Interfaces;
using SocialSiteCommonLayer.ResponseModels;
using SocialSiteRepositoryLayer.Interfaces;
using System;

namespace SocialSiteBusinessLayer.Services
{
    public class PostBusiness : IPostBusiness
    {
        private readonly IPostRepository _postRepository;

        public PostBusiness(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public PostResponse UploadImage(int userID, string postPath)
        {
            if (userID > 0 && postPath != null)
                return _postRepository.UploadImage(userID, postPath);
            else
                return null;
        }
    }
}
