using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MTNet.Core
{
    public static class ContentRepository
    {
        static ContentRepository()
        {
            Content = new ContentList();
            DisallowedContent = new List<string>();
            Populate();
        }

        public static DateTime LastPopulated { get; private set; }

        public static List<string> DisallowedContent { get; private set; }

        public static ContentItem HomePage
        {
            get { return GetContentForPath(String.Empty); }
        }

        public static ContentList Content { get; private set; }

        public static void Refresh()
        {
            Content.Clear();
            Populate();
            LastPopulated = DateTime.MinValue;
        }

        public static void Populate()
        {
            XDocument contentXml = XDocument.Parse(File.ReadAllText(Utilities.CombinePath(Configuration.Instance.DataPath, Configuration.Instance.ManifestFileName)));
            foreach (XElement element in contentXml.Descendants("content"))
            {
                string path = Utilities.NormalizePath(element.Attribute("url").Value);

                // Does this appear in the list of disallowed paths?
                if (Configuration.Instance.DisallowedPaths.Contains(path))
                {
                    // Yep, skip it...
                    if (DisallowedContent == null)
                    {
                        DisallowedContent = new List<string>();
                    }
                    DisallowedContent.Add(path);
                    continue;
                }

                ContentType type = element.Attribute("type").Value == "page" ? ContentType.Page : ContentType.Entry;

                var page = new ContentItem(path, type);
                Content.Add(page);
            }

            LastPopulated = DateTime.Now;
        }

        public static ContentItem GetContentForPath(string path)
        {
            path = Utilities.NormalizePath(path);

            if (Content.All(i => i.Path != path))
            {
                // Nothing matches the path
                return null;
            }
            return Content.First(i => i.Path == path);
        }

        public static ContentList GetContentAtLevel(int level)
        {
            return Content.Where(i => i.Path.Count(f => f == '/') == level).ToContentList();
        }

        public static ContentList GetContentAtLevel(ContentLevel level)
        {
            return GetContentAtLevel((int) level);
        }
    }
}