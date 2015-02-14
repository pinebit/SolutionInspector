using System.Collections.Generic;
using System.Linq;

using SolutionInspector.Contracts;

namespace SolutionInspector.Rules
{
    internal class AllowBuildEventsRule : IRule
    {
        public IEnumerable<IIssue> Apply(ISolution solution)
        {
            foreach (IProject project in solution.Projects)
            {
                if (project.Settings.AllowBuildEvents ?? true)
                {
                    continue;
                }

                if (project.Properties.Any(p => p.Name == "PreBuildEvent"))
                {
                    yield return Issue.Create(Issue.NoBuildEventsAllowed, project, "Project {0} is not allowed to have PreBuildEvent", project.Name);
                }

                if (project.Properties.Any(p => p.Name == "PostBuildEvent"))
                {
                    yield return Issue.Create(Issue.NoBuildEventsAllowed, project, "Project {0} is not allowed to have PostBuildEvent", project.Name);
                }
            }
        } 
    }
}