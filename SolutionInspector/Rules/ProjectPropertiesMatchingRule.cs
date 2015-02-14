using System.Collections.Generic;
using System.Linq;

using SolutionInspector.Contracts;

namespace SolutionInspector.Rules
{
    internal class ProjectPropertiesMatchingRule : IRule
    {
        public IEnumerable<IIssue> Apply(ISolution solution)
        {
            foreach (IProject project in solution.Projects)
            {
                if (project.Settings.Properties == null)
                {
                    continue;
                }

                Dictionary<string, IEnumerable<string>> projectProperties = project.Properties.ToDictionary(GetPropertyKey, y => y.Values);

                foreach (IProjectProperty property in project.Settings.Properties)
                {
                    string key = GetPropertyKey(property);
                    string flatList = string.Join(";", property.Values);

                    if (!projectProperties.ContainsKey(key))
                    {
                        yield return Issue.Create(Issue.ProjectPropertyMissing, project, "Required Property {0}={1} is missing", key, flatList);
                        continue;
                    }

                    List<string> actualValues = projectProperties[key].ToList();
                    if (!actualValues.SequenceEqual(property.Values))
                    {
                        yield return Issue.Create(Issue.ProjectPropertyMismatch, project, "Property {0} value mismatch, expected: {1}", key, flatList);
                    }
                }
            }
        }

        private static string GetPropertyKey(IProjectProperty property)
        {
            List<string> items = new List<string> { property.Name };

            if (property.Configuration != null)
            {
                items.Add("Configuration=" + property.Configuration);
            }

            if (property.Platform != null)
            {
                items.Add("Platform=" + property.Platform);
            }

            return string.Join(", ", items);
        }
    }
}