using System.Collections.Generic;

using SolutionInspector.Contracts;

namespace SolutionInspector.Model
{
    internal class Project : IProject
    {
        public Project(
            string file,
            string name,
            IProjectSettings settings,
            IEnumerable<string> files,
            IEnumerable<IProjectProperty> properties,
            IEnumerable<string> imports)
        {
            File = file;
            Name = name;
            Settings = settings;
            Files = files;
            Properties = properties;
            Imports = imports;
        }

        public string File { get; private set; }

        public string Name { get; private set; }

        public IProjectSettings Settings { get; private set; }

        public IEnumerable<string> Files { get; private set; }

        public IEnumerable<IProjectProperty> Properties { get; private set; }

        public IEnumerable<string> Imports { get; private set; }
    }
}