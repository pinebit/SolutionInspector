namespace SolutionCop.Core.Settings
{
    using System.Collections.Generic;

    using SolutionCop.Contracts.Settings;

    internal class ProjectProperty : IProjectProperty
    {
        public ProjectProperty(string configuration, string platform, string name, IEnumerable<string> values)
        {
            Configuration = configuration;
            Platform = platform;
            Name = name;
            Values = values;
        }

        public string Configuration { get; private set; }

        public string Platform { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<string> Values { get; private set; }
    }
}