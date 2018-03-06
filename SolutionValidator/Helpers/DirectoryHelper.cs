using System.IO;
using System.Linq;
using System.Threading;

namespace SolutionValidator
{
    internal static class DirectoryHelper
    {
        public static void Delete(string path)
        {
            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
                return;

            foreach (var file in directory.GetFiles("*", SearchOption.AllDirectories).Select(f => f.FullName).Concat(
                directory.GetDirectories("*", SearchOption.AllDirectories).Select(f => f.FullName)))
            {
                try
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                }
                catch
                {
                }
            }

            var attempts = 0;
            while (directory.Exists)
            {
                try
                {
                    directory.Delete(true);
                    break;
                }
                catch (IOException)
                {
                    if (attempts > 100)
                        throw;
                }
                Thread.Sleep(100);
                attempts++;
            }
        }

        public static void PrepareClean(string path)
        {
            Delete(path);
            new DirectoryInfo(path).Create();
        }

        public static void Copy(string source, string destination)
        {
            Directory.CreateDirectory(destination);

            foreach (string dirPath in Directory.GetDirectories(source, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(source, destination));

            foreach (string newPath in Directory.GetFiles(source, "*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(source, destination), true);
        }
    }
}