namespace SolutionValidator
{
    internal class PreparedSolution
    {
        public string BinariesDirectory { get; }

        public string ExecutablePath { get; }

        public PreparedSolution(string binariesDirectory, string executablePath)
        {
            BinariesDirectory = binariesDirectory;
            ExecutablePath = executablePath;
        }
    }
}