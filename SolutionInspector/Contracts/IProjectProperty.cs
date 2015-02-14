using System.Collections.Generic;

namespace SolutionInspector.Contracts
{
    public interface IProjectProperty
    {
        string Configuration { get; }

        string Platform { get; }

        string Name { get; }

        IEnumerable<string> Values { get; }
    }
}
