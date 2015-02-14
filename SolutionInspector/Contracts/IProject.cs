using System.Collections.Generic;

namespace SolutionInspector.Contracts
{
    public interface IProject
    {
        string File { get; }

        string Name { get; }

        IProjectSettings Settings { get; }

        IEnumerable<string> Files { get; }

        IEnumerable<IProjectProperty> Properties { get; }

        IEnumerable<string> Imports { get; }
    }
}