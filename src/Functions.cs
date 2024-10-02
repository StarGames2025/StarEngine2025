using System;
using System.IO;

namespace StarEngine2025
{
    public class PathHelper
    {
        public static string RelativePathMaker(string relativePath)
        {
            string combinedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            return Path.GetFullPath(combinedPath);
        }
    }
}