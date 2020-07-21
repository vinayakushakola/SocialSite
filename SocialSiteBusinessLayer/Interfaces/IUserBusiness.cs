//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain Definition of UserBusiness Methods
//

using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;

namespace SocialSiteBusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        UserResponse Registration(RegistrationRequest userDetails);

        UserResponse Login(LoginRequest loginDetails);
    }
}
