namespace SolutionCop.Core
{
    using SolutionCop.Contracts;
    using SolutionCop.Contracts.Model;

    internal class Issue : IIssue
    {
        public const string WrongSolutionFormatVersion = "SC101";

        public const string MissingFileDetected = "SC201";

        public const string MissingRequiredImport = "SC202";

        public const string ProjectVsFileNameMismatch = "SC301";

        public const string ProjectNamePrefixMismatch = "SC302";

        public const string AssemblyVsProjectNameMismatch = "SC303";

        public const string NamespaceVsAssemblyNameMismatch = "SC304";

        public const string NoBuildEventsAllowed = "SC401";

        public const string ProjectPropertyMissing = "SC501";

        public const string ProjectPropertyMismatch = "SC502";

        private Issue(string code, string file, string text)
        {
            Code = code;
            File = file;
            Text = text;
        }

        public string Code { get; private set; }

        public string File { get; private set; }

        public string Text { get; private set; }

        public static IIssue Create(string code, ISolution solution, string format, params object[] args)
        {
            return Create(code, solution.File, format, args);
        }

        public static IIssue Create(string code, IProject project, string format, params object[] args)
        {
            return Create(code, project.File, format, args);
        }

        public static IIssue Create(string code, string filepath, string format, params object[] args)
        {
            return new Issue(code, filepath, string.Format(format, args));
        }
    }
}
