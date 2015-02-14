using System;

using SolutionInspector.Contracts;

namespace SolutionInspector.Exceptions
{
    public class ProjectReaderException : Exception
    {
        public ProjectReaderException(IProject project, string message)
            : base(string.Format("Project file {0} read error: {1}", project.File, message))
        {
        }
    }
}
