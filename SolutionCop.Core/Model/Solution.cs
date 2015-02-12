namespace SolutionCop.Core.Model
{
    using System;
    using System.Collections.Generic;

    using SolutionCop.Contracts.Model;
    using SolutionCop.Contracts.Settings;

    internal class Solution : ISolution
    {
        public Solution(
            string file,
            Version formatVersion,
            ISolutionSettings settings,
            IEnumerable<string> files,
            IEnumerable<IProject> projects)
        {
            File = file;
            FormatVersion = formatVersion;
            Settings = settings;
            Files = files;
            Projects = projects;
        }

        public string File { get; private set; }

        public Version FormatVersion { get; private set; }

        public ISolutionSettings Settings { get; private set; }

        public IEnumerable<string> Files { get; private set; }

        public IEnumerable<IProject> Projects { get; private set; }
    }
}