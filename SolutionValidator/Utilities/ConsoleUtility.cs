using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SolutionValidator
{
    internal class ConsoleUtility
    {
        public ConsoleUtility(string exePath)
        {
            this.exePath = exePath;

            utilityName = Path.GetFileName(exePath);
        }

        public bool Run(string commandline, string workingDirectory)
        {
            Console.WriteLine($"Running {utilityName} with commandline: {commandline}");

            var utilityProcess = Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = commandline,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory(),
                UseShellExecute = false
            });

            if (utilityProcess == null)
                throw new Exception($"Could not start {utilityName}");

            var stdoutReader = ReadProcessOutput(utilityProcess.StandardOutput);
            var stderrReader = ReadProcessOutput(utilityProcess.StandardError);

            utilityProcess.WaitForExit();

            Console.WriteLine($"{utilityName} output:{Environment.NewLine}{stdoutReader.Result}{Environment.NewLine}{stderrReader.Result}");

            if (utilityProcess.ExitCode != 0)
            {
                Console.WriteLine($"{utilityName} exited with non-successful code {utilityProcess.ExitCode}");

                return false;
            }

            return true;

        }

        private static async Task<string> ReadProcessOutput(StreamReader outputReader)
        {
            var stream = new MemoryStream();

            await outputReader.BaseStream.CopyToAsync(stream);

            stream.Seek(0, SeekOrigin.Begin);

            return new StreamReader(stream).ReadToEnd();
        }

        private readonly string exePath;
        private readonly string utilityName;
    }
}