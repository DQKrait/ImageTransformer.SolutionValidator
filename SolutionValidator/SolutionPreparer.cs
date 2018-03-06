using System;
using System.IO;
using System.IO.Compression;

namespace SolutionValidator
{
    internal class SolutionPreparer
    {
        public PreparedSolution PrepareSolution(string archivePath, string msbuildPath)
        {
            Console.WriteLine($"Preparing solution '{Path.GetFileName(archivePath)}'...");

            var outputDirectory = Path.ChangeExtension(archivePath, "");

            Console.WriteLine($"Cleaning solution directory '{outputDirectory}'...");

            DirectoryHelper.PrepareClean(outputDirectory);

            ZipFile.ExtractToDirectory(archivePath, outputDirectory);

            Console.WriteLine("Rebuilding solution...");

            var sourceDirectory = PathHelper.GetSourceDirectory(outputDirectory);

            new SolutionBuilder(msbuildPath).Build(sourceDirectory);

            var exePath = PathHelper.GetExePath(outputDirectory);

            if (exePath == null)
                throw new Exception("No exe file was found");

            Console.WriteLine("Solution prepared successfully.");

            return new PreparedSolution(Directory.GetParent(exePath).FullName, exePath);
        }
    }
}