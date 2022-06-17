using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Core.Base;

namespace Gitbang.ViewModels.Windows
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {

        }

        public string WindowTitle
        {
            get => Get<string>();
            private set => Set(value);
        }
    }
}
