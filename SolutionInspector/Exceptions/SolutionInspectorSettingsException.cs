using System;

using SolutionInspector.Settings.Persistence;

namespace SolutionInspector.Exceptions
{
    public class SolutionInspectorSettingsException : Exception
    {
        public SolutionInspectorSettingsException(string settingsFile, string message)
            : base(string.Format("{0} file error: {1}, see {2}", SettingsReader.SettingsFileName, message, settingsFile))
        {
        }
    }
}
