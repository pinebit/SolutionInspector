using System.Collections.Generic;
using System.IO;
using System.Linq;

using SolutionInspector.Contracts;

namespace SolutionInspector.Rules
{
    internal class DetectMissingFilesRule : IRule
    {
        public IEnumerable<IIssue> Apply(ISolution solution)
        {
            if (solution.Settings.DetectMissingFiles ?? false)
            {
                string directory = Path.GetDirectoryName(solution.File);
                foreach (string file in CheckFiles(directory, solution.Files))
                {
                    yield return Issue.Create(Issue.MissingFileDetected, solution, "Solution references missing file: {0}", file);
                }
            }

            foreach (IProject project in solution.Projects)
            {
                if (project.Settings.DetectMissingFiles ?? false)
                {
                    string directory = Path.GetDirectoryName(project.File);
                    foreach (string file in CheckFiles(directory, project.Files))
                    {
                        yield return Issue.Create(Issue.MissingFileDetected, project, "Project references missing file: {0}", file);
                    }
                }
            }
        }

        private static IEnumerable<string> CheckFiles(string directory, IEnumerable<string> files)
        {
            return files.Where(file => !File.Exists(Path.Combine(directory, file)));
        }
    }
}
