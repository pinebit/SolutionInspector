namespace SolutionCop.Core.Model.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using SolutionCop.Contracts.Model;
    using SolutionCop.Contracts.Settings;
    using SolutionCop.Core.Model;
    using SolutionCop.Core.Settings;
    using SolutionCop.Core.Settings.Persistence;

    internal static class ProjectReader
    {
        public static IProject Read(string name, IProjectSettings projectSettings, string path)
        {
            string directory = Path.GetDirectoryName(path);
            IProjectSettings settings = SettingsReader.ReadProjectSettings(directory) ?? projectSettings;

            XElement root = XElement.Load(path);

            IEnumerable<string> files = GetFiles(root).ToList();
            IEnumerable<IProjectProperty> properties = GetProperties(root).ToList();
            IEnumerable<string> imports = GetImports(root).ToList();

            return new Project(path, name, settings, files, properties, imports);
        }

        private static IEnumerable<IProjectProperty> GetProperties(XElement root)
        {
            var groups = root.Descendants(root.Name.Namespace + "PropertyGroup");
            foreach (XElement @group in groups)
            {
                string configuration = null;
                string platform = null;

                XAttribute conditionAttribute = @group.Attribute("Condition");
                if (conditionAttribute != null)
                {
                    string[] parts = conditionAttribute.Value.Split("'| ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 5)
                    {
                        configuration = parts[3];
                        platform = parts[4];
                    }
                }

                foreach (XElement property in @group.Elements())
                {
                    string[] values = property.Value.Split(new[] { ',', ';' });
                    yield return new ProjectProperty(configuration, platform, property.Name.LocalName, values);
                }
            }
        }

        private static IEnumerable<string> GetFiles(XElement root)
        {
            var all = root.Descendants(root.Name.Namespace + "Compile")
                          .Concat(root.Descendants(root.Name.Namespace + "Content")
                          .Concat(root.Descendants(root.Name.Namespace + "None")));

            return all.Select(element => element.Attribute("Include").Value);
        }

        private static IEnumerable<string> GetImports(XElement root)
        {
            var all = root.Descendants(root.Name.Namespace + "Import");
            
            return all.Select(element => element.Attribute("Project").Value);
        }
    }
}
