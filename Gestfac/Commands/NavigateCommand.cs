using Gestfac.Services;
using Gestfac.Stores;
using Gestfac.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Commands
{
    public class NavigateCommand<T> : CommandBase where T : ViewModelBase
    {
        private readonly NavigationService<T> _navigationService;

        public NavigateCommand(NavigationService<T> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
