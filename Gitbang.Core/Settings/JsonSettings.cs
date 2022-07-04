using Gitbang.Core.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Core.Settings
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class JsonSettings : ModelBase
    {
        internal static T Empty<T>() where T : JsonSettings
        {
            return (T)Activator.CreateInstance<T>().Empty();
        }

        protected abstract JsonSettings Empty();
    }

}
