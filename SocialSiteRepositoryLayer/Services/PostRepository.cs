//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain Implementation of IPostRepository Methods
//

using SocialSiteCommonLayer.DBModels;
using SocialSiteCommonLayer.ResponseModels;
using SocialSiteRepositoryLayer.ApplicationContext;
using SocialSiteRepositoryLayer.Interfaces;
using System;

namespace SocialSiteRepositoryLayer.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDBContext _appDB;
        private static int count = 0;

        public PostRepository(AppDBContext appDB)
        {
            _appDB = appDB;
        }

        public PostResponse UploadImage(int userID, string postPath)
        {
            try
            {
                var postData = new Posts
                {
                    UserID = userID,
                    PostPath = postPath,
                    IsRemoved = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                _appDB.Posts.Add(postData);
                count = _appDB.SaveChanges();
                if (count > 0)
                {
                    var responseData = new PostResponse
                    {
                        PostID = postData.PostID,
                        UserID = postData.UserID,
                        PostPath = postData.PostPath,
                        IsRemoved = postData.IsRemoved,
                        CreatedDate = postData.CreatedDate,
                        ModifiedDate = postData.ModifiedDate
                    };
                    return responseData;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
