using System.ComponentModel.DataAnnotations;

namespace YOY.UserAPI.Models.v1.ThirdpartyLogin.Facebook.POCO
{
    public class FbSDKData
    {
        [Required]
        public string FbToken { get; set; }

        public override string ToString()
        {
            return "FBToken: " + FbToken;
        }
    }
}
