using System.IO;

namespace SolutionValidator
{
    internal static class PathHelper
    {
        public static string GetSourceDirectory(string baseDirectory)
        {
            return baseDirectory;
        }

        public static string GetSolutionPath(string sourceDirectory)
        {
            return Path.Combine(sourceDirectory, "Kontur.ImageTransformer.sln");
        }

        public static string GetExePath(string baseDirectory)
        {
            return Path.Combine(baseDirectory, @"Kontur.ImageTransformer\bin\Release\Kontur.ImageTransformer.exe");
        }

        public static string NugetPath => @"External\nuget.exe";
    }
}