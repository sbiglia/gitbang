using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Core;
using Gitbang.Core.Base;
using Gitbang.Core.Models;

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

        public IEnumerable<ThemeModel> Themes => ThemeManager.Instance.Themes;
    }
}
