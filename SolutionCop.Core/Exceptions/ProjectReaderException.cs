namespace SolutionCop.Core.Exceptions
{
    using System;

    using SolutionCop.Contracts.Model;

    public class ProjectReaderException : Exception
    {
        public ProjectReaderException(IProject project, string message)
            : base(string.Format("Project file {0} read error: {1}", project.File, message))
        {
        }
    }
}
