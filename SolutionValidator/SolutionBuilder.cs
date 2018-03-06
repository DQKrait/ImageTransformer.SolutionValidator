using System;

namespace SolutionValidator
{
    internal class SolutionBuilder
    {
        private readonly string msbuildPath;

        public SolutionBuilder(string msbuildPath)
        {
            this.msbuildPath = msbuildPath;
        }

        public void Build(string sourceDirectory)
        {
            var solutionFile = PathHelper.GetSolutionPath(sourceDirectory);

            var nuget = new NuGet();

            nuget.RestoreSolutionPackages(solutionFile);

            var msbuild = new MsBuild(msbuildPath);
            
            if (!msbuild.BuildProjectOrSolution(solutionFile))
                throw new Exception("Failed to build solution");
        }
    }
}