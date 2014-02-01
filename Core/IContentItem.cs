using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTNet.Core
{
    public interface IContentItem
    {
        string Path { get; set; }
        string Title { get; }
        string Body { get; }
        string Excerpt { get; }
        DateTime CreatedOn { get; }
        DateTime LastModified { get; }
        List<string> Tags { get; }
        ContentType Type { get; set; }
        string Url { get; }
        ContentList Children { get; }
        IContentItem Parent { get; }
        int Depth { get; }
        ContentList Ancestors { get; }
        ContentList Descendants { get; }
        bool IsHome { get; }
        List<string> AllowedRoles { get; }
        bool IsAncestorOf(IContentItem contentItem);
        IContentItem GetAncestorAtLevel(ContentLevel level);
        bool HasTag(string tag);
    }
}

