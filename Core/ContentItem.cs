using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MTNet.Core
{
    [DebuggerDisplay("{Path}")]
    public class ContentItem
    {
        public ContentItem(string path, ContentType type)
        {
            Tags = new List<string>();
            AllowedRoles = new List<string>();

            Path = path;
            Type = type;
            Populate();
        }

        public string Path { get; set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string Excerpt { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime LastModified { get; private set; }
        public List<string> Tags { get; private set; }
        public ContentType Type { get; set; }

        public string Url
        {
            get { return Path; }
        }

        private string DataFilePath
        {
            get
            {
                string filename = Utilities.CombinePath(Configuration.Instance.DataPath, Path);
                if (File.Exists(filename))
                {
                    return filename;
                }

                return Utilities.CombinePath(filename, Configuration.Instance.DefaultFileName);
            }
        }

        private ContentList children;
        public ContentList Children
        {
            get
            {
                if (children == null)
                {
                    children = ContentRepository.Content.Where(i => i.Path.StartsWith(Path) && i.Path.Count(f => f == '/') == Path.Count(f => f == '/') + 1).ToContentList();
                }
                return children;
            }
        }

        // TODO: This logic is broken in the sense that it requires an IMMEDIATE parent. So, if you have a/b and /a/b/c/d, "d" would not have a parent, since it's looking for something that the third directory segment (the missing "c").  This logic can't "skip" a segment.
        private ContentItem parent;
        public ContentItem Parent
        {
            get
            {
                if (parent == null)
                {

                    // This is the top of the tree. Nowhere to go from here...
                    if (String.IsNullOrWhiteSpace(Path))
                    {
                        return null;
                    }

                    string parentPath = String.Join("/", Path.Split('/').Take(Path.Split('/').Count() - 1));

                    if (ContentRepository.Content.ContainsKey(parentPath))
                    {
                        parent = ContentRepository.Content[parentPath];
                    }
                }

                return parent;
            }
        }

        public int Depth
        {
            get { return Ancestors.Count; }
        }

        private ContentList ancestors;
        public ContentList Ancestors
        {
            get
            {
                if (ancestors == null)
                {
                    ancestors = new ContentList();
                    var currentItem = this;
                    while (true)
                    {
                        ancestors.Add(currentItem);
                        if (currentItem.Parent == null)
                        {
                            break;
                        }
                        currentItem = currentItem.Parent;
                    }
                }
                return ancestors;
            }
        }

        public ContentList Descendants
        {
            get { return ContentRepository.Content.Where(ci => ci.Path.StartsWith(String.Concat(Path, "/"))).ToContentList(); }
        }

        public bool IsHome
        {
            get { return Path == String.Empty; }
        }

        public List<string> AllowedRoles { get; private set; }

        private void Populate()
        {
            XDocument xml = XDocument.Parse(File.ReadAllText(DataFilePath));

            Title = xml.Descendants("title").First().Value;
            Body = xml.Descendants("body").First().Value;
            Excerpt = xml.Descendants("excerpt").First().Value;
            CreatedOn = DateTime.Parse(xml.Descendants("createdOn").First().Value);
            LastModified = DateTime.Parse(xml.Descendants("lastModified").First().Value);
            Tags = xml.Descendants("tag").Select(e => e.Value).ToList();

            // TODO: There's got to be a way to LINQ this up
            if (xml.Descendants("allowedRoles").Any())
            {
                foreach (string role in xml.Descendants("allowedRoles").First().Value.Split(','))
                {
                    if (String.IsNullOrWhiteSpace(role))
                    {
                        continue;
                    }
                    AllowedRoles.Add(role);
                }
            }
        }

        public bool IsAncestorOf(ContentItem contentItem)
        {
            if (contentItem == null)
            {
                return false;
            }

            return (contentItem.Path.StartsWith(Path));
        }

        public ContentItem GetAncestorAtLevel(ContentLevel level)
        {
            return Ancestors[(int) level - 1];
        }

        public bool HasTag(string tag)
        {
            return Tags.Contains(tag);
        }
    }
}