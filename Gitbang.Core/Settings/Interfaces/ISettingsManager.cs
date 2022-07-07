using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Core.Settings.Interfaces
{
    public interface ISettingsManager
    {
        public ISessionSettings SessionSettings { get; }
        public void SaveConfiguration();
    }
}
