using SocialSiteCommonLayer.ResponseModels;
//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Definition of PostBusiness Methods
//

namespace SocialSiteBusinessLayer.Interfaces
{
    public interface IPostBusiness
    {
        PostResponse UploadImage(int userID, string postPath);
    }
}
