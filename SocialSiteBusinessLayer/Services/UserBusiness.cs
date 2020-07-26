//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain Implementation of IUserBusiness Methods
//

using SocialSiteBusinessLayer.Interfaces;
using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;
using SocialSiteRepositoryLayer.Interfaces;
using System.Collections.Generic;

namespace SocialSiteBusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserResponse> ListOfUsers()
        {
            return _userRepository.ListOfUsers();
        }

        public UserResponse Registration(RegistrationRequest userDetails)
        {
            if (!userDetails.Equals(null))
                return _userRepository.Registration(userDetails);
            else
                return null;
        }

        public UserResponse Login(LoginRequest loginDetails)
        {
            if (!loginDetails.Equals(null))
                return _userRepository.Login(loginDetails);
            else
                return null;
        }

        public bool SendFriendRequest(int userID, int friendID)
        {
            if (userID > 0 && friendID > 0)
                return _userRepository.SendFriendRequest(userID, friendID);
            else
                return false;
        }

        public List<UserResponse> FriendRequests(int userID)
        {
            if (userID > 0)
                return _userRepository.FriendRequests(userID);
            else
                return null;
        }

        public bool AcceptFriendRequest(int userID, int friendID)
        {
            if (userID > 0 && friendID > 0)
                return _userRepository.AcceptFriendRequest(userID, friendID);
            else
                return false;
        }

        public bool RejectFriendRequest(int userID, int friendID)
        {
            if (userID > 0 && friendID > 0)
                return _userRepository.RejectFriendRequest(userID, friendID);
            else
                return false;
        }
    }
}
