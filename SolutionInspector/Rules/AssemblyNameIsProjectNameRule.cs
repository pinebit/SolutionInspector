using System.Collections.Generic;
using System.Linq;

using SolutionInspector.Contracts;

namespace SolutionInspector.Rules
{
    internal class AssemblyNameIsProjectNameRule : IRule
    {
        public IEnumerable<IIssue> Apply(ISolution solution)
        {
            foreach (IProject project in solution.Projects)
            {
                if (project.Settings.AssemblyNameIsProjectName ?? false)
                {
                    IProjectProperty assemblyNameProperty = project.Properties.FirstOrDefault(p => p.Name == "AssemblyName");
                    if (assemblyNameProperty == null)
                    {
                        continue;
                    }

                    string assemblyName = assemblyNameProperty.Values.FirstOrDefault() ?? string.Empty;
                    if (assemblyName != project.Name)
                    {
                        yield return Issue.Create(Issue.AssemblyVsProjectNameMismatch, project, "AssemblyName {0} is not matching Project name {1}", assemblyName, project.Name);
                    }
                }
            }
        } 
    }
}