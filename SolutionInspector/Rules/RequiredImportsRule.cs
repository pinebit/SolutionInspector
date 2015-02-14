using System.Collections.Generic;
using System.IO;
using System.Linq;

using SolutionInspector.Contracts;

namespace SolutionInspector.Rules
{
    internal class RequiredImports : IRule
    {
        public IEnumerable<IIssue> Apply(ISolution solution)
        {
            foreach (IProject project in solution.Projects)
            {
                if (project.Settings.RequiredImports == null)
                {
                    continue;
                }

                foreach (string requiredImport in project.Settings.RequiredImports)
                {
                    if (project.Imports.All(x => Path.GetFileName(x) != requiredImport))
                    {
                        yield return Issue.Create(Issue.MissingRequiredImport, project, "Project has missing target import: {0}", requiredImport);
                    }
                }
            }
        } 
    }
}