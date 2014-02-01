using System;
using System.Web;

namespace MTNet.Core
{
    public class ContentFilter : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.BeginRequest += FilterForContentPath;
        }

        public void Dispose()
        {
        }

        /// <summary>
        ///     Determine if we have content for the inbound path. If so, retrieve it and redirect to the content controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterForContentPath(object sender, EventArgs e)
        {
            HttpRequest request = ((HttpApplication) sender).Context.Request;
            HttpContext context = ((HttpApplication) sender).Context;

            // Do we have content for this path?
            ContentItem contentItem = ContentRepository.GetContentForPath(request.Url.AbsolutePath);
            if (contentItem == null)
            {
                // No, just let this request pass-through
                return;
            }

            // Add this content item to the context (the controller will retrieve it from here)
            context.Items.Add("ContentItem", contentItem);

            // Redirect to the content controller
            context.RewritePath(Utilities.CombinePath("/", Configuration.Instance.ContentControllerName, Configuration.Instance.ControllerActionName));
        }
    }
}