using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Core.Services
{
    public interface IEnvironmentService
    {
        string GetApplicationDataDirectory();

        string GetIntanceDirectory();
    }
}
