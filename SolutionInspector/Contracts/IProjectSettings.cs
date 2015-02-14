using System.Collections.Generic;

namespace SolutionInspector.Contracts
{
    public interface IProjectSettings
    {
        bool? DetectMissingFiles { get; }

        bool? AllowBuildEvents { get; }

        bool? AssemblyNameIsProjectName { get; }

        bool? RootNamespaceIsAssemblyName { get; }

        IEnumerable<string> RequiredImports { get; }
            
        IEnumerable<IProjectProperty> Properties { get; }
    }
}
