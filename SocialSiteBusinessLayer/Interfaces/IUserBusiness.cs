//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain Definition of UserBusiness Methods
//

using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;
using System.Collections.Generic;

namespace SocialSiteBusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        List<UserResponse> ListOfUsers();
        
        UserResponse Registration(RegistrationRequest userDetails);

        UserResponse Login(LoginRequest loginDetails);

        bool SendFriendRequest(int userID, int friendID);
    }
}
