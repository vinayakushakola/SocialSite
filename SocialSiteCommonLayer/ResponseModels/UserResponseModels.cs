//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain User Response Models
//

using System;

namespace SocialSiteCommonLayer.ResponseModels
{
    public class UserResponse
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ProfilePath { get; set; }

        public bool IsActive { get; set; }

        public string UserRole { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
