using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EducationWPF.Infrastructure.Commands;
using EducationWPF.ViewModels.Base;

namespace EducationWPF.ViewModels
{
    internal class WebServerViewModel :ViewModel
    {
        #region Enabled

        private bool _enabled = false;

        public bool Enabled
        {
            get => _enabled;
            set => Set(ref _enabled, value);
        }

        #endregion

        #region Strat command

        private ICommand _startCommand;

        public ICommand StartCommand => _startCommand ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

        private bool CanStartCommandExecute(object p) => !Enabled;

        private void OnStartCommandExecuted(object p)
        {
            Enabled = true;
        }

        #endregion

        #region Stop command

        private ICommand _stopCommand;

        public ICommand StopCommand => _stopCommand ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

        private bool CanStopCommandExecute(object p) => Enabled;

        private void OnStopCommandExecuted(object p)
        {
            Enabled = false;
        }

        #endregion
    }
}
