//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain Definition of UserRepository Methods
//

using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;

namespace SocialSiteRepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        UserResponse Registration(RegistrationRequest userDetails);
        
        UserResponse Login(LoginRequest loginDetails);
    }
}
