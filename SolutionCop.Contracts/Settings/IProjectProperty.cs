namespace SolutionCop.Contracts.Settings
{
    using System.Collections.Generic;

    public interface IProjectProperty
    {
        string Configuration { get; }

        string Platform { get; }

        string Name { get; }

        IEnumerable<string> Values { get; }
    }
}
