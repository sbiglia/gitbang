using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Core.Settings.Interfaces
{
    public interface IConfigurationManager
    {
        public ISessionSettings SessionSettings { get; }
        public void SaveConfiguration();
    }
}
