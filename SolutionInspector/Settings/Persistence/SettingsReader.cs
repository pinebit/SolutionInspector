using System.IO;
using System.Xml.Linq;

using SolutionInspector.Contracts;

namespace SolutionInspector.Settings.Persistence
{
    internal static class SettingsReader
    {
        public const string SettingsFileName = "SolutionInspector.xml";

        public static ISolutionSettings ReadSolutionSettings(string directory)
        {
            string uri = Path.Combine(directory, SettingsFileName);
            if (File.Exists(uri))
            {
                XElement root = XElement.Load(uri);
                XElement solutionSettings = Utils.GetSingleOptionalElement(uri, root, "SolutionSettings");
                if (solutionSettings != null)
                {
                    return SolutionSettingsReader.Read(uri, solutionSettings);
                }
            }

            return new SolutionSettings();
        }

        public static IProjectSettings ReadProjectSettings(string directory)
        {
            string uri = Path.Combine(directory, SettingsFileName);
            if (File.Exists(uri))
            {
                XElement root = XElement.Load(uri);
                XElement projectSettings = Utils.GetSingleOptionalElement(uri, root, "ProjectSettings");
                if (projectSettings != null)
                {
                    return ProjectSettingsReader.Read(uri, projectSettings);
                }
            }

            return null;
        }
    }
}