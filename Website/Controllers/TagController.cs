using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StaticContent;
using Website.Models;

namespace Website.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Tag/

        public ActionResult Index(string tag)
        {
            tag = WebUtility.UrlDecode(tag);

            var model = new ContentViewModel();
            model.CurrentList = ContentRepository.Content.Where(ci => ci.Tags.Contains(tag)).OrderByDescending(ci => ci.CreatedOn).ToContentList();
            model.PageTitle = String.Format("Items tagged as \"{0}\"", tag);

            return View(model);
        }

    }
}
