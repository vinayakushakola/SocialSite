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
    }
}
