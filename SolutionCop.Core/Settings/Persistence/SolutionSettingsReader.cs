namespace SolutionCop.Core.Settings.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using SolutionCop.Contracts.Settings;
    using SolutionCop.Core.Exceptions;

    internal static class SolutionSettingsReader
    {
        public static ISolutionSettings Read(string settingsFile, XElement element)
        {
            bool? detectMissingFiles = Utils.ReadOptionalFlag(settingsFile, element, "DetectMissingFiles");
            bool? projectNameIsFileName = Utils.ReadOptionalFlag(settingsFile, element, "ProjectNameIsFileName");

            IEnumerable<string> ignoredProjects = Utils.ReadOptionalList(settingsFile, element, "IgnoredProjects");

            Version maxSolutionFormatVersion = ReadOptionalVersion(settingsFile, element, "MaxSolutionFormatVersion");
            Version minSolutionFormatVersion = ReadOptionalVersion(settingsFile, element, "MinSolutionFormatVersion");

            XElement version = Utils.GetSingleOptionalElement(settingsFile, element, "MinSolutionFormatVersion");
            if (version != null)
            {
                minSolutionFormatVersion = Version.Parse(version.Value);
            }

            XElement projectNamePattern = Utils.GetSingleOptionalElement(settingsFile, element, "ProjectNamePrefix");

            return new SolutionSettings(
                maxSolutionFormatVersion,
                minSolutionFormatVersion,
                detectMissingFiles,
                projectNamePattern == null ? null : projectNamePattern.Value,
                projectNameIsFileName,
                ignoredProjects);
        }

        private static Version ReadOptionalVersion(string settingsFile, XElement root, string name)
        {
            XElement versionElement = Utils.GetSingleOptionalElement(settingsFile, root, name);
            if (versionElement == null)
            {
                return null;
            }

            Version version;
            if (Version.TryParse(versionElement.Value, out version))
            {
                return version;
            }

            throw new SolutionCopSettingsException(settingsFile, "Version value " + versionElement.Value + " has wrong format");
        }
    }
}
