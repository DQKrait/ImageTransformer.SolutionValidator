using System;
using System.IO;

namespace SolutionValidator
{
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2 || !File.Exists(args[0]) || !File.Exists(args[1]))
            {
                Console.WriteLine("Usage:");
                Console.WriteLine($"\t{AppDomain.CurrentDomain.FriendlyName} <path-to-solution.zip> <path-to-msbuild.exe>");
                Console.WriteLine("Example:");
                Console.WriteLine($@"{"\t"}{AppDomain.CurrentDomain.FriendlyName} solution.zip ""C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\amd64\MSBuild.exe""");
                return;
            }

            try
            {
                var solution = new SolutionPreparer().PrepareSolution(args[0], args[1]);

                using (var controller = new SolutionController(solution))
                {
                    controller.Start();

                    new SolutionTester(controller.PublicUrl).Test();
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
        }
    }
}