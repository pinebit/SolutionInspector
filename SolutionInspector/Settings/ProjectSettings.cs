using System.Collections.Generic;

using SolutionInspector.Contracts;

namespace SolutionInspector.Settings
{
    internal class ProjectSettings : IProjectSettings
    {
        public ProjectSettings(
            bool? detectMissingFiles,
            bool? allowBuildEvents,
            bool? assemblyNameIsProjectName,
            bool? rootNamespaceIsAssemblyName,
            IEnumerable<string> requiredImports,
            IEnumerable<IProjectProperty> properties)
        {
            DetectMissingFiles = detectMissingFiles;
            AllowBuildEvents = allowBuildEvents;
            AssemblyNameIsProjectName = assemblyNameIsProjectName;
            RootNamespaceIsAssemblyName = rootNamespaceIsAssemblyName;
            RequiredImports = requiredImports;
            Properties = properties;
        }

        public bool? DetectMissingFiles { get; private set; }

        public bool? AllowBuildEvents { get; private set; }

        public bool? AssemblyNameIsProjectName { get; private set; }

        public bool? RootNamespaceIsAssemblyName { get; private set; }

        public IEnumerable<string> RequiredImports { get; private set; }

        public IEnumerable<IProjectProperty> Properties { get; private set; }
    }
}