using System.Collections.Generic;

using SolutionInspector.Contracts;

namespace SolutionInspector.Rules
{
    internal class ProjectNamePrefixRule : IRule
    {
        public IEnumerable<IIssue> Apply(ISolution solution)
        {
            if (solution.Settings.ProjectNamePrefix == null)
            {
                yield break;
            }

            foreach (IProject project in solution.Projects)
            {
                if (!project.Name.StartsWith(solution.Settings.ProjectNamePrefix))
                {
                    yield return
                        Issue.Create(
                            Issue.ProjectNamePrefixMismatch,
                            project,
                            "Project name {0} is not following pattern: {1}",
                            project.Name,
                            solution.Settings.ProjectNamePrefix);
                }
            }
        } 
    }
}