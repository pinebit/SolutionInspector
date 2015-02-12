namespace SolutionCop.Core.Rules
{
    using System.Collections.Generic;

    using SolutionCop.Contracts;

    public static class RulesRepository
    {
        public static IEnumerable<IRule> GetAllRules()
        {
            yield return new SolutionFormatVersionRule();
            yield return new DetectMissingFilesRule();
            yield return new ProjectNameIsFileNameRule();
            yield return new ProjectNamePrefixRule();
            yield return new RequiredImports();
            yield return new AllowBuildEventsRule();
            yield return new AssemblyNameIsProjectNameRule();
            yield return new RootNamespaceIsAssemblyNameRule();
            yield return new ProjectPropertiesMatchingRule();
        }
    }
}