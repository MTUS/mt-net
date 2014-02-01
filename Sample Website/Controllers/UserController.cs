using System.Web.Mvc;
using System.Web.Security;
using Website.Models;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View(new ContentViewModel());
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            if (form["role"] != "Anonymous")
            {
                Membership.ValidateUser("VIP Customer", "password");
                FormsAuthentication.SetAuthCookie("VIP Customer", false);
            }

            return Redirect("/user/login");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();

            return Redirect("/");
        }
    }
}