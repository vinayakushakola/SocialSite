﻿//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain Definition of UserRepository Methods
//

using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;
using System.Collections.Generic;

namespace SocialSiteRepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        List<UserResponse> ListOfUsers();

        UserResponse Registration(RegistrationRequest userDetails);
        
        UserResponse Login(LoginRequest loginDetails);

        bool SendFriendRequest(int userID, int friendID);

        List<UserResponse> FriendRequests(int userID);

        bool AcceptFriendRequest(int userID, int friendID);
    }
}
