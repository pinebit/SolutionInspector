using System;
using System.Xml.Linq;

using SolutionInspector.Contracts;

namespace SolutionInspector.Settings.Persistence
{
    internal static class ProjectPropertyReader
    {
        public static IProjectProperty Read(XElement element)
        {
            XNamespace ns = element.Name.Namespace;

            XAttribute attribute = element.Attribute(ns + "Configuration");
            string configuration = attribute == null ? null : attribute.Value;

            attribute = element.Attribute(ns + "Platform");
            string platform = attribute == null ? null : attribute.Value;

            string[] values = element.Value.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            return new ProjectProperty(configuration, platform, element.Name.LocalName, values);
        }
    }
}
