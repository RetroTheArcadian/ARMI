using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ARMI.Helpers
{
    public static class DirectoryWithMultipleFileExtensionFilters
    {
        // Works in .Net 4.0 - inferred overloads with default values
        public static IEnumerable<string> GetFiles(string path, string searchPatternExpression = "", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(path)) return Enumerable.Empty<string>();
            Regex reSearchPattern = new Regex(searchPatternExpression);
            return Directory.EnumerateFiles(path, "*", searchOption).Where(file => reSearchPattern.IsMatch(Path.GetFileName(file)));
        }

        // Works in .Net 4.0 - takes same patterns as old method, and executes in parallel
        public static IEnumerable<string> GetFiles(string path, string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(path)) return Enumerable.Empty<string>();
            return searchPatterns.AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(path, searchPattern, searchOption)).Select(Path.GetFileNameWithoutExtension);
        }
    }
}
