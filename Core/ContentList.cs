using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace MTNet.Core
{
    public class ContentList : ICollection<ContentItem>
    {
        private Collection<ContentItem> items;

        public ContentList()
        {
            Init();
        }

        public ContentList(IEnumerable<ContentItem> contentItems)
        {
            Init();
            contentItems.ToList().ForEach(items.Add);
        }

        private Collection<ContentItem> filteredItems
        {
            get
            {
                if (Filter)
                {
                    return new Collection<ContentItem>(items.Where(i => IsAuthorized(i)).ToList());
                }
                return items;
            }
        }

        public ContentItem this[int i]
        {
            get { return filteredItems[i]; }
        }

        public bool Filter { get; set; }

        public ContentItem this[string key]
        {
            get { return filteredItems.First(i => i.Path == key); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(ContentItem item)
        {
            items.Add(item);
        }

        public bool Remove(ContentItem item)
        {
            return items.Remove(item);
        }

        public void CopyTo(ContentItem[] x, int y)
        {
            filteredItems.CopyTo(x, y);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(ContentItem item)
        {
            return items.Contains(item);
        }

        public int Count
        {
            get { return filteredItems.Count(); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return filteredItems.GetEnumerator();
        }

        public IEnumerator<ContentItem> GetEnumerator()
        {
            foreach (ContentItem item in filteredItems)
            {
                yield return item;
            }
        }

        private bool IsAuthorized(ContentItem item)
        {
            if (item.AllowedRoles.Count == 0)
            {
                // This content is public
                return true;
            }

            // Content is not public and they're not logged in, so they can't be authorized
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                return false;
            }

            // If the AllowedRoles of this item includes their current identity, return true
            return item.AllowedRoles.Contains(HttpContext.Current.User.Identity.Name);
        }

        private void Init()
        {
            Filter = true;
            items = new Collection<ContentItem>();
        }

        public bool ContainsKey(string key)
        {
            return filteredItems.Any(i => i.Path == key);
        }

        public ContentList GetReverse()
        {
            // LINQ's Reverse method sucks, because it reverses in place, which is not what I want. I just want to iterate one time as reversed...

            var reversedItems = new ContentList();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                reversedItems.Add(items[i]);
            }
            return reversedItems;
        }
    }

    public static class ContentListExtension
    {
        public static ContentList ToContentList(this IEnumerable<ContentItem> items)
        {
            return new ContentList(items);
        }
    }
}