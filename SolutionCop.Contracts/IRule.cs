namespace SolutionCop.Contracts
{
    using System.Collections.Generic;
    using SolutionCop.Contracts.Model;

    public interface IRule
    {
        IEnumerable<IIssue> Apply(ISolution solution);
    }
}
