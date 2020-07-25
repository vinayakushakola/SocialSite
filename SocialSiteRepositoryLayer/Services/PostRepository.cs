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
using System.Collections.Generic;
using System.Linq;

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

        public List<PostResponse> ListOfPosts(int userID)
        {
            try
            {
                List<PostResponse> responseData = null;
                var userExists = _appDB.Users.Any(user => user.ID == userID && user.UserRole == "User");
                if (userExists)
                {
                    responseData = _appDB.Posts.
                        Where(post => post.UserID == userID && post.IsRemoved == false).
                        Join(_appDB.Users,
                        p => p.UserID,
                        u => u.ID,
                        (p, v) => new PostResponse
                        {
                            PostID = p.PostID,
                            UserID = p.UserID,
                            PostPath = p.PostPath,
                            IsRemoved = p.IsRemoved,
                            PostLikes = p.PostLikes,
                            CreatedDate = p.CreatedDate,
                            ModifiedDate = p.ModifiedDate
                        }).ToList();
                    if (responseData != null)
                        return responseData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PostResponse GetPostByID(int userID, int postID)
        {
            try
            {
                var userExists = CheckUserExists(userID);
                if (userExists)
                {
                    var postData = _appDB.Posts.
                        Where(post => post.PostID == postID && post.IsRemoved == false).
                        FirstOrDefault();
                    var responseData = PostResponseMethod(postData);
                    return responseData;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                    var responseData = PostResponseMethod(postData);
                    return responseData;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool LikePost(int userID, int postID)
        {
            try
            {
                var likeData = new Likes
                {
                    PostID = postID,
                    LikeByUserID = userID,
                    IsLiked = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                var postExists = _appDB.Posts.Any(post => post.PostID == postID);
                if (postExists)
                {
                    var postData = _appDB.Posts.Find(postID);
                    postData.PostLikes += 1;
                    _appDB.SaveChanges();
                    _appDB.Likes.Add(likeData);
                    count = _appDB.SaveChanges();
                    if (count > 0)
                        return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private PostResponse PostResponseMethod(Posts postData)
        {
            try
            {
                var responseData = new PostResponse
                {
                    PostID = postData.PostID,
                    UserID = postData.UserID,
                    PostPath = postData.PostPath,
                    PostLikes = postData.PostLikes,
                    IsRemoved = postData.IsRemoved,
                    CreatedDate = postData.CreatedDate,
                    ModifiedDate = postData.ModifiedDate
                };
                return responseData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool CheckUserExists(int userID)
        {
            try
            {
                return _appDB.Users.Any(user => user.ID == userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
