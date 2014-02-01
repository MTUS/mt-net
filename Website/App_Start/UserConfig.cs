using System.Web.Security;

namespace Website
{
    public class UserConfig
    {
        public static void RegisterGlobalUsers()
        {
            Membership.CreateUser("VIP Customer", "haosfin;WOIENF;oisef");
        }
    }
}