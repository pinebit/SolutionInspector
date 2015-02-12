namespace SolutionCop.Core.Exceptions
{
    using System;

    using SolutionCop.Core.Settings.Persistence;

    public class SolutionCopSettingsException : Exception
    {
        public SolutionCopSettingsException(string settingsFile, string message)
            : base(string.Format("{0} file error: {1}, see {2}", SettingsReader.SettingsFileName, message, settingsFile))
        {
        }
    }
}
