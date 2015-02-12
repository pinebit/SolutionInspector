namespace SolutionCop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using SolutionCop.Contracts;
    using SolutionCop.Contracts.Model;
    using SolutionCop.Core.Model.Persistence;
    using SolutionCop.Core.Rules;

    public static class Program
    {
        public static void Main(string[] args)
        {
            PrintGreeting();

            if (args.Length == 0)
            {
                PrintUsage();
                Environment.Exit(0);
            }

            string slnFile = Path.GetFullPath(args[0]);
            if (!File.Exists(slnFile))
            {
                Console.WriteLine("Specified parameter is expected to be an existing Visual Studio solution file.");
                Console.WriteLine("Parameter: {0}", args[0]);
                Environment.Exit(-1);
            }

            Console.WriteLine("Analyzing solution {0}", slnFile);
            Console.WriteLine();

            ISolution solution = SolutionReader.Read(slnFile);
            List<IIssue> issues = new List<IIssue>();

            foreach (IRule rule in RulesRepository.GetAllRules())
            {
                issues.AddRange(rule.Apply(solution));
            }

            if (issues.Count == 0)
            {
                Console.WriteLine("No issues were found.");
            }
            else
            {
                string lastFile = null;
                foreach (IIssue issue in issues.OrderBy(x => x.File))
                {
                    if (lastFile != issue.File)
                    {
                        Console.WriteLine("File: {0}", issue.File);
                        lastFile = issue.File;
                    }

                    Console.WriteLine("[{0}] {1}", issue.Code, issue.Text);
                }

                Environment.ExitCode = -1;
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: SolutionCop.exe [masterpiece.sln]");
            Console.WriteLine();
        }

        private static void PrintGreeting()
        {
            Console.WriteLine("SolutionCop tool for Visual Studio solution structure and settings validation.");
            Console.WriteLine("Developed by Andrei Smirnov (c) 2015, please check GitHub for updates.");
            Console.WriteLine();
        }
    }
}
