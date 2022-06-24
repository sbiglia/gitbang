using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace Gitbang
{
    static class Constants
    {
        public const string AppName = "GitBang";
        public const string AppDescrioption = "";

        public static readonly Version Version;
        public static readonly string DataDirectory;


        static Constants()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(0,0,0,0);
            DataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);
        }
        
    }

    public enum TreeNodeType : byte
    {
        Group,
        Repository
    }
	
}
