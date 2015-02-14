using System;
using System.Collections.Generic;

namespace SolutionInspector.Contracts
{
    public interface ISolution
    {
        string File { get; }

        Version FormatVersion { get; }

        ISolutionSettings Settings { get; }

        IEnumerable<string> Files { get; }
        
        IEnumerable<IProject> Projects { get; }
    }
}