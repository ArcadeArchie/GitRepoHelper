﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitRepoHelper.Util
{
    public static class DirectoryHelper
    {
        public static bool DoesPathExist(string targetPath, out bool isFilePath)
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


    }
}