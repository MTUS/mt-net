using System.Web.Mvc;
using MTNet.Core;
using Website.Models;

namespace Website.Controllers
{
    public class ContentController : Controller
    {
        private ContentItem InboundContentItem
        {
            get { return (ContentItem) System.Web.HttpContext.Current.Items["ContentItem"]; }
        }

        public ActionResult Refresh()
        {
            ContentRepository.Refresh();
            return RedirectToAction("All");
        }

        public ActionResult Tag(string path)
        {
            var model = new ContentViewModel();
            return View(model);
        }

        public ActionResult Display(string path)
        {
            var model = new ContentViewModel
            {
                CurrentItem = InboundContentItem
            };

            return View("~/Views/Content/" + InboundContentItem.Type + ".cshtml", model);
        }

        public ActionResult All(string path)
        {
            var model = new ContentViewModel();
            return View(model);
        }
    }
}