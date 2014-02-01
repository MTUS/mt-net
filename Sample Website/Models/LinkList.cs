using MTNet.Core;

namespace Website.Models
{
    public class LinkList
    {
        public LinkList(ContentList contentItems)
        {
            ContentItems = contentItems;
        }

        public ContentList ContentItems { get; private set; }
        public ContentItem CurrentItem { get; set; }
        public bool Recursive { get; set; }
        public bool SuppressOuterTag { get; set; }
        public string Delimiter { get; set; }
    }
}