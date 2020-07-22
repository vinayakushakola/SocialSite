//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Definition of PostRepository Methods
//

using SocialSiteCommonLayer.ResponseModels;

namespace SocialSiteRepositoryLayer.Interfaces
{
    public interface IPostRepository
    {
        PostResponse UploadImage(int userID, string postPath);
    }
}
