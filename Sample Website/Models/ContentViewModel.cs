using MTNet.Core;

namespace Website.Models
{
    public class ContentViewModel
    {
        private ContentItem currentItem;

        public ContentViewModel()
        {
        }

        public ContentViewModel(ContentItem contentIem)
        {
            CurrentItem = contentIem;
        }

        public ContentItem CurrentItem
        {
            get
            {
                if (currentItem == null)
                {
                    return ContentRepository.HomePage;
                }
                return currentItem;
            }
            set { currentItem = value; }
        }

        public ContentList CurrentList;

        private string pageTitle;

        public string PageTitle
        {
            get { return pageTitle ?? CurrentItem.Title; }
            set { pageTitle = value;  }

        }
    }
}