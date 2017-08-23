using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SolutionInspector.Contracts;
using SolutionInspector.Settings.Persistence;

namespace SolutionInspector.Model.Persistence
{
    public static class SolutionReader
    {
        private const string Prefix = "Microsoft Visual Studio Solution File, Format Version";

        private const string ProjectTag = "Project";

        private const string ProjectSectionTag = "ProjectSection";

        private const string EndProjectTag = "EndProject";

        private const string EndProjectSectionTag = "EndProjectSection";

        public static ISolution Read(string path)
        {
            string directory = Path.GetDirectoryName(path);
            ISolutionSettings solutionSettings = SettingsReader.ReadSolutionSettings(directory);
            IProjectSettings projectSettings = SettingsReader.ReadProjectSettings(directory);
            
            string[] lines = File.ReadAllLines(path)
                                 .Select(x => x.Trim())
                                 .Where(x => !string.IsNullOrEmpty(x))
                                 .ToArray();

            Version formatVersion = ReadFormatVersion(lines);
            List<string> files = new List<string>();
            IEnumerable<IProject> projects = ReadProjects(lines, directory, solutionSettings.IgnoredProjects, projectSettings, files);

            return new Solution(path, formatVersion, solutionSettings, files, projects.ToList());
        }

        private static string RemoveQuotes(string text)
        {
            return text.Trim().Trim(new[] { '"' });
        }

        private static Version ReadFormatVersion(IEnumerable<string> lines)
        {
            string formatLine = lines.First(x => x.StartsWith(Prefix));
            string rawVersion = formatLine.Substring(Prefix.Length).Trim();
            return Version.Parse(rawVersion);
        }

        private static IEnumerable<IProject> ReadProjects(
            IEnumerable<string> lines,
            string directory,
            IEnumerable<string> ignoredProjects,
            IProjectSettings projectSettings,
            ICollection<string> files)
        {
            HashSet<string> ignoreSet = new HashSet<string>();
            if (ignoredProjects != null)
                ignoreSet.SetEquals(ignoredProjects);
            Context context = Context.None;

            foreach (string line in lines)
            {
                switch (GetTag(line))
                {
                    case ProjectTag:
                        context = Context.Project;
                        break;

                    case EndProjectSectionTag:
                        context = Context.Project;
                        continue;

                    case EndProjectTag:
                        context = Context.None;
                        continue;

                    case ProjectSectionTag:
                        context = Context.ProjectSection;
                        continue;
                }

                if (context == Context.Project)
                {
                    string[] parts = line.Split(new[] { '=', ',' });
                    string projectName = RemoveQuotes(parts[1]);
                    string projectFile = RemoveQuotes(parts[2]);

                    if (projectName != projectFile && !ignoreSet.Contains(projectName))
                    {
                        files.Add(projectFile);
                        string path = Path.Combine(directory, projectFile);
                        yield return ProjectReader.Read(projectName, projectSettings, path);
                    }
                }

                if (context == Context.ProjectSection)
                {
                    string[] parts = line.Split('=');
                    files.Add(parts[1].Trim());
                }
            }
        }

        private static string GetTag(string line)
        {
            string tag = line;
            int open = line.IndexOf('(');
            if (open > 0)
            {
                tag = line.Substring(0, open);
            }

            return tag;
        }

        private enum Context
        {
            None,
            Project,
            ProjectSection
        }
    }
}