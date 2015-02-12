namespace SolutionCop.Core.Exceptions
{
    using System;

    using SolutionCop.Contracts.Model;

    public class SolutionReaderException : Exception
    {
        public SolutionReaderException(ISolution solution, string message)
            : base(string.Format("Solution file {0} read error: {1}", solution.File, message))
        {
        }
    }
}
