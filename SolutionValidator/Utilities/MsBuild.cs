using System.IO;

namespace SolutionValidator
{
    internal class MsBuild
    {
        public MsBuild(string msbuildPath)
        {
            utility = new ConsoleUtility(msbuildPath);
        }

        public bool BuildProjectOrSolution(string projectOrSolution)
        {
            var commandline = $"/p:Configuration=Release /p:AllowUnsafeBlocks=true /p:Utf8Output=true \"{Path.GetFileName(projectOrSolution)}\"";

            return utility.Run(commandline, Path.GetDirectoryName(projectOrSolution));
        }

        private readonly ConsoleUtility utility;
    }
}