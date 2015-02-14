using System;
using System.Collections.Generic;

using SolutionInspector.Contracts;

namespace SolutionInspector.Settings
{
    internal class SolutionSettings : ISolutionSettings
    {
        public SolutionSettings(
            Version maxSolutionFormatVersion,
            Version minSolutionFormatVersion,
            bool? detectMissingFiles,
            string projectNamePrefix,
            bool? projectNameIsFileName,
            IEnumerable<string> ignoredProjects)
        {
            MaxSolutionFormatVersion = maxSolutionFormatVersion;
            MinSolutionFormatVersion = minSolutionFormatVersion;
            DetectMissingFiles = detectMissingFiles;
            ProjectNamePrefix = projectNamePrefix;
            ProjectNameIsFileName = projectNameIsFileName;
            IgnoredProjects = ignoredProjects;
        }

        public SolutionSettings()
        {
        }

        public Version MaxSolutionFormatVersion { get; private set; }

        public Version MinSolutionFormatVersion { get; private set; }

        public bool? DetectMissingFiles { get; private set; }

        public string ProjectNamePrefix { get; private set; }

        public bool? ProjectNameIsFileName { get; private set; }

        public IEnumerable<string> IgnoredProjects { get; private set; }
    }
}