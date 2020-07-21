//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain Implementation of IUserBusiness Methods
//

using SocialSiteBusinessLayer.Interfaces;
using SocialSiteCommonLayer.RequestModels;
using SocialSiteCommonLayer.ResponseModels;
using SocialSiteRepositoryLayer.Interfaces;

namespace SocialSiteBusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}
