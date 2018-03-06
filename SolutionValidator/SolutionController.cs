using System;
using System.Diagnostics;
using System.IO;

namespace SolutionValidator
{
    internal class SolutionController : IDisposable
    {
        public SolutionController(PreparedSolution solution)
        {
            this.solution = solution;
        }

        public string PublicUrl => "http://localhost:8080/";

        public void Dispose()
        {
            Stop();
        }

        public bool Start()
        {
            solutionProcess = Process.Start(new ProcessStartInfo
            {
                FileName = solution.ExecutablePath,
                CreateNoWindow = false,
                UseShellExecute = false,
                WorkingDirectory = solution.BinariesDirectory
            });

            if (solutionProcess == null)
            {
                Console.WriteLine($"Failed to start solution {Path.GetFileName(solution.ExecutablePath)}.");

                return false;
            }

            Console.WriteLine($"Started solution {Path.GetFileName(solution.ExecutablePath)}.");

            return true;
        }

        public bool Stop()
        {
            try
            {
                if (solutionProcess == null)
                    return true;

                solutionProcess.Kill();

                solutionProcess = null;

                Console.WriteLine($"Stopped solution {Path.GetFileName(solution.ExecutablePath)}.");

                return true;
            }
            catch (InvalidOperationException)
            {
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine($"Failed to kill solution process {Path.GetFileName(solution.ExecutablePath)}: {error}");

                return false;
            }
        }

        public bool Restart()
        {
            return Stop() && Start();
        }

        private Process solutionProcess;

        private readonly PreparedSolution solution;
    }
}