using System.Collections.Generic;
using System.Linq;

using SolutionInspector.Contracts;

namespace SolutionInspector.Rules
{
    internal class RootNamespaceIsAssemblyNameRule : IRule
    {
        public IEnumerable<IIssue> Apply(ISolution solution)
        {
            foreach (IProject project in solution.Projects)
            {
                if (project.Settings.AssemblyNameIsProjectName ?? false)
                {
                    IProjectProperty assemblyNameProperty = project.Properties.FirstOrDefault(p => p.Name == "AssemblyName");
                    IProjectProperty rootNamespaceProperty = project.Properties.FirstOrDefault(p => p.Name == "RootNamespace");
                    if (assemblyNameProperty == null || rootNamespaceProperty == null)
                    {
                        continue;
                    }

                    string assemblyName = assemblyNameProperty.Values.FirstOrDefault() ?? string.Empty;
                    string rootNamespace = rootNamespaceProperty.Values.FirstOrDefault() ?? string.Empty;
                    if (assemblyName != rootNamespace)
                    {
                        yield return Issue.Create(Issue.NamespaceVsAssemblyNameMismatch, project, "RootNamespace {0} is not matching AssemblyName {0}", rootNamespace, assemblyName);
                    }
                }
            }
        } 
    }
}