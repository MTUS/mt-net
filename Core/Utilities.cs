using System;
using System.Diagnostics;
using System.Linq;

namespace MTNet.Core
{
    public static class Utilities
    {
        public static string CombinePath(params string[] pathElements)
        {
            return String.Join("/", pathElements.Where(s => !String.IsNullOrWhiteSpace(s)).Select(s => s.TrimStart('/').TrimEnd('/')));
        }

        [DebuggerHidden]
        public static string NormalizePath(string rawPath)
        {
            // Make it relative
            if (rawPath.StartsWith("http"))
            {
                var url = new Uri(rawPath);
                rawPath = url.AbsolutePath;
            }

            // Replace backslashes
            rawPath = rawPath.Replace(@"\", "/");

            // In case the path has a trailing slash
            rawPath = rawPath.TrimEnd('/');

            // In case the path has a leading slash
            //rawPath = rawPath.TrimStart('/');

            // In case the path has mixed case
            rawPath = rawPath.ToLower();

            return rawPath;
        }
    }
}