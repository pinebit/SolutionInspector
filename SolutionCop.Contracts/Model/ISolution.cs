namespace SolutionCop.Contracts.Model
{
    using System;
    using System.Collections.Generic;

    using SolutionCop.Contracts.Settings;

    public interface ISolution
    {
        string File { get; }

        Version FormatVersion { get; }

        ISolutionSettings Settings { get; }

        IEnumerable<string> Files { get; }
        
        IEnumerable<IProject> Projects { get; }
    }
}