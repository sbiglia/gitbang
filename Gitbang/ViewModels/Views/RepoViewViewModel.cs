using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gitbang.Core.Base;
using ReactiveUI;

namespace Gitbang.ViewModels.Views
{
    public class RepoViewViewModel : ViewModelBase
    {

        public ICommand LoadRepoCommand;
        
        public RepoViewViewModel()
        {
            LoadRepoCommand = ReactiveCommand.Create(LoadRepo);
        }

        public void LoadRepo()
        {

        }



    }
}
