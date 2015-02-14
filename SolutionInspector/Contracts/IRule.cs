using System.Collections.Generic;

namespace SolutionInspector.Contracts
{
    public interface IRule
    {
        IEnumerable<IIssue> Apply(ISolution solution);
    }
}
