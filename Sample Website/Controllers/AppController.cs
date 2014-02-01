using System.Web.Mvc;

namespace Website.Controllers
{
    public class AppController : Controller
    {
        //
        // GET: /App/

        public ActionResult Do()
        {
            return Content("<h1>This is the app controller.  I know nothing about the content in the system.</h1>");
        }
    }
}