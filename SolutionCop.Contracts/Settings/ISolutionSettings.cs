namespace SolutionCop.Contracts.Settings
{
    using System;
    using System.Collections.Generic;

    public interface ISolutionSettings
    {
        Version MaxSolutionFormatVersion { get; }

        Version MinSolutionFormatVersion { get; }

        bool? DetectMissingFiles { get; }

        string ProjectNamePrefix { get; }

        bool? ProjectNameIsFileName { get; }

        IEnumerable<string> IgnoredProjects { get; }
    }
}
