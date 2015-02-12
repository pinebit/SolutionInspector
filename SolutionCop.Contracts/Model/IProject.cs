namespace SolutionCop.Contracts.Model
{
    using System.Collections.Generic;

    using SolutionCop.Contracts.Settings;

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