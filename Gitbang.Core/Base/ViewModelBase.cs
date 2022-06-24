using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;

namespace Gitbang.Core.Base
{
    public abstract class ViewModelBase : ModelBase
    {

        protected ViewModelBase()
        {

        }


        protected bool IsDesignMode => Design.IsDesignMode;
    }
}
