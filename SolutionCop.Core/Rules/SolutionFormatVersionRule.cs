namespace SolutionCop.Core.Rules
{
    using System.Collections.Generic;

    using SolutionCop.Contracts;
    using SolutionCop.Contracts.Model;

    internal class SolutionFormatVersionRule : IRule
    {
        public IEnumerable<IIssue> Apply(ISolution solution)
        {
            if (solution.Settings.MinSolutionFormatVersion != null
                && solution.FormatVersion < solution.Settings.MinSolutionFormatVersion)
            {
                yield return
                    Issue.Create(
                        Issue.WrongSolutionFormatVersion,
                        solution,
                        "Solution format version {0} is less than {1}",
                        solution.FormatVersion,
                        solution.Settings.MinSolutionFormatVersion);
            }

            if (solution.Settings.MaxSolutionFormatVersion != null
                && solution.FormatVersion > solution.Settings.MaxSolutionFormatVersion)
            {
                yield return
                    Issue.Create(
                        Issue.WrongSolutionFormatVersion,
                        solution,
                        "Solution format version {0} is greater than {1}",
                        solution.FormatVersion,
                        solution.Settings.MaxSolutionFormatVersion);
            }
        }
    }
}
