using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EducationWPF.Infrastructure.Commands;
using EducationWPF.Services.Interfaces;
using EducationWPF.ViewModels.Base;

namespace EducationWPF.ViewModels
{
    internal class WebServerViewModel :ViewModel
    {
        private readonly IWebServerService _server;

        #region Enabled
        public bool Enabled
        {
            get => _server.Enabled;
            set
            {
                _server.Enabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Strat command

        private ICommand _startCommand;

        public ICommand StartCommand => _startCommand ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

        private bool CanStartCommandExecute(object p) => !Enabled;

        private void OnStartCommandExecuted(object p)
        {
            _server.Start();
            OnPropertyChanged(nameof(Enabled));
        }

        #endregion

        #region Stop command

        private ICommand _stopCommand;

        public ICommand StopCommand => _stopCommand ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

        private bool CanStopCommandExecute(object p) => Enabled;

        private void OnStopCommandExecuted(object p)
        {
            _server.Stop();
            OnPropertyChanged(nameof(Enabled));
        }

        #endregion


        #region ctor

        public WebServerViewModel(IWebServerService server)
        {
            _server = server;
        }
        #endregion
    }
}
