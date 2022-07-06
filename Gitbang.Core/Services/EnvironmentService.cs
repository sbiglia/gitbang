using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Gitbang.Core.Services
{
    public class EnvironmentService: IEnvironmentService
    {
        public string GetApplicationDataDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GitBang");
        }

        public string GetIntanceDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
