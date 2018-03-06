using System.IO;

namespace SolutionValidator
{
    internal class NuGet
    {
        public NuGet()
        {
            utility = new ConsoleUtility(PathHelper.NugetPath);
        }

        public bool RestoreSolutionPackages(string solutionFile)
        {
            var commandline = $"restore -NonInteractive \"{Path.GetFileName(solutionFile)}\"";

            return utility.Run(commandline, Path.GetDirectoryName(solutionFile));
        }

        public bool RestoreProjectPackages(string packagesConfigFile)
        {
            var commandline = $"restore -NonInteractive -SolutionDirectory .. \"{Path.GetFileName(packagesConfigFile)}\"";

            return utility.Run(commandline, Path.GetDirectoryName(packagesConfigFile));
        }

        private readonly ConsoleUtility utility;
    }
}