//
// Author  : Vinayak Ushakola
// Date    : 21/07/2020
// Purpose : It Contain Encode Method
//

using System;
using System.Text;

namespace SocialSiteCommonLayer.CommonClasses
{
    public class EncodeDecode
    {

        /// <summary>
        /// It Encrypt the Password to Base64
        /// </summary>
        /// <param name="password">User Password</param>
        /// <returns>Encrypted Password</returns>
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
