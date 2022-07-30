using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Gitbang.Core.Helpers
{
    public static class EnvironmentData
    {
        public static string GetApplicationDataDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GitBang");
        }

        public static string GetIntanceDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
