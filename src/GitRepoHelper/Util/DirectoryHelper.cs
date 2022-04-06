using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitRepoHelper.Util
{
    public static class DirectoryHelper
    {
        public static bool DoesPathExist(string? targetPath, out bool isFilePath)
        {
            if (string.IsNullOrEmpty(targetPath))
            {
                isFilePath = false;
                return false;
            }
            isFilePath = Path.HasExtension(targetPath);
            targetPath = ToFullPath(targetPath);

            return isFilePath ? File.Exists(targetPath) : Directory.Exists(targetPath);
        }

        public static string ToFullPath(string targetPath)
        {
            if (Path.IsPathFullyQualified(targetPath))
                return targetPath;
            return Path.GetFullPath(targetPath);
        }

        public static IEnumerable<string> GetChildren(string path) =>
            Directory.EnumerateDirectories(path);

        public static string GetParentDirFromFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new InvalidOperationException("The given path must not be null or empty");
            return Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException("The given path is not valid");
        }
    }
}
