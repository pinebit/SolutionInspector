using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using SolutionInspector.Exceptions;

namespace SolutionInspector.Settings.Persistence
{
    internal static class Utils
    {
        public static IEnumerable<string> ReadOptionalList(string settingsFile, XElement root, string name)
        {
            XElement list = GetSingleOptionalElement(settingsFile, root, name);
            if (list == null)
            {
                return null;
            }

            return list.Value.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
        }

        public static bool? ReadOptionalFlag(string settingsFile, XElement root, string name)
        {
            XElement flag = GetSingleOptionalElement(settingsFile, root, name);
            if (flag == null)
            {
                return null;
            }

            try
            {
                return bool.Parse(flag.Value.ToLowerInvariant());
            }
            catch (Exception)
            {
                throw new SolutionInspectorSettingsException(settingsFile, "Boolean setting " + name + " has wrong value " + flag.Value);
            }
        }

        public static XElement GetSingleOptionalElement(string settingsFile, XElement root, string name)
        {
            XElement[] elements = root.Elements(root.Name.Namespace + name).ToArray();
            if (elements.Length == 0)
            {
                return null;
            }

            if (elements.Length == 1)
            {
                return elements[0];
            }

            throw new SolutionInspectorSettingsException(settingsFile, "Element " + name + " must appear once");
        }
    }
}
