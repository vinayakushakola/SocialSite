//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain Implementation of IUserRepository Methods
//

using SocialSiteCommonLayer.CommonClasses;
using SocialSiteCommonLayer.DBModels;
using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;
using SocialSiteRepositoryLayer.ApplicationContext;
using SocialSiteRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialSiteRepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _appDB;
        private readonly string _user = "User";
        private static int count = 0;

        public UserRepository(AppDBContext appDB)
        {
            _appDB = appDB;
        }

        public List<UserResponse> ListOfUsers()
        {
            try
            {
                List<UserResponse> userResponses = null;
                userResponses = _appDB.Users.
                    Where(user => user.IsActive == true && user.UserRole == _user).
                    Select(user => new UserResponse
                    {
                        ID = user.ID,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        IsActive = user.IsActive,
                        UserRole = user.UserRole,
                        CreatedDate = user.CreatedDate,
                        ModifiedDate = user.ModifiedDate
                    }).ToList();
                if (userResponses != null)
                    return userResponses;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserResponse Registration(RegistrationRequest userDetails)
        {
            try
            {
                UserResponse userResponse = null;
                var emailExists = _appDB.Users.Any(user => user.Email == userDetails.Email);
                if (!emailExists)
                {
                    userDetails.Password = EncodeDecode.EncodePasswordToBase64(userDetails.Password);
                    var userData = new Users
                    {
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName,
                        Email = userDetails.Email,
                        Password = userDetails.Password,
                        IsActive = true,
                        UserRole = _user,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    _appDB.Users.Add(userData);
                    count = _appDB.SaveChanges();

                    if (count > 0)
                    {
                        userResponse = UserResponseMethod(userData);
                        return userResponse;
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserResponse Login(LoginRequest loginDetails)
        {
            try
            {
                UserResponse userResponse = null;
                loginDetails.Password = EncodeDecode.EncodePasswordToBase64(loginDetails.Password);
                var userData = _appDB.Users.
                    Where(user => user.Email == loginDetails.Email && user.Password == loginDetails.Password).
                    FirstOrDefault();
                if (userData != null)
                {
                    userResponse = UserResponseMethod(userData);
                    return userResponse;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SendFriendRequest(int userID, int friendID)
        {
            try
            {
                var userExists = CheckUserExists(userID);
                var friendExists = CheckUserExists(friendID);
                if(userExists && friendExists)
                {
                    var friendData = new Friends
                    {
                        UserID = userID,
                        FriendID = friendID,
                        IsAccepted = false,
                        IsRejected = false,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    _appDB.Friends.Add(friendData);
                    count = _appDB.SaveChanges();
                    if (count > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserResponse> FriendRequests(int userID)
        {
            try
            {
                List<UserResponse> userResponses = null;
                var userExists = CheckUserExists(userID);
                if (userExists)
                {
                    userResponses = _appDB.Friends.
                        Where(user => user.FriendID == userID && user.IsAccepted == false && user.IsRejected == false).
                        Join(_appDB.Users,
                        f => f.UserID,
                        u => u.ID,
                        (f, u) => UserResponseMethod(u)).ToList();
                    if (userResponses != null) 
                        return userResponses;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AcceptFriendRequest(int userID, int friendID)
        {
            try
            {
                var userExists = CheckUserExists(userID);
                if (userExists)
                {
                    var friendData = _appDB.Friends.
                        Where(friend => friend.FriendID == userID && friend.UserID == friendID).FirstOrDefault();
                    if (friendData != null)
                    {
                        friendData.IsAccepted = true;
                        count = _appDB.SaveChanges();
                        if (count > 0)
                            return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RejectFriendRequest(int userID, int friendID)
        {
            try
            {
                var userExists = CheckUserExists(userID);
                if (userExists)
                {
                    var friendData = _appDB.Friends.
                        Where(friend => friend.FriendID == userID && friend.UserID == friendID).FirstOrDefault();
                    if (friendData != null)
                    {
                        friendData.IsRejected = true;
                        count = _appDB.SaveChanges();
                        if (count > 0)
                            return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private UserResponse UserResponseMethod(Users userData)
        {
            try
            {
                UserResponse responseData = new UserResponse
                {
                    ID = userData.ID,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    Email = userData.Email,
                    IsActive = userData.IsActive,
                    UserRole = userData.UserRole,
                    CreatedDate = userData.CreatedDate,
                    ModifiedDate = userData.ModifiedDate
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool CheckUserExists(int userID)
        {
            try
            {
                var userExists = _appDB.Users.Any(user => user.ID == userID);
                if (userExists)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
