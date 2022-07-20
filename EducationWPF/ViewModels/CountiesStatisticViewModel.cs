using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EducationWPF.Infrastructure.Commands;
using EducationWPF.Models;
using EducationWPF.Services;
using EducationWPF.ViewModels.Base;

namespace EducationWPF.ViewModels
{
    internal class CountiesStatisticViewModel : ViewModel
    {
        private DataService _DataService;

        private MainWindowViewModel MainModel { get; }


        #region Countries : IEnymeravle<CountryInfo> 

        private IEnumerable<CountryInfo> _Countries;

        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            private set => Set(ref _Countries, value);
        }

        #endregion



        #region Commands

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommand(object p)
        {
            Countries = _DataService.GetData();
        }

        #endregion


        public CountiesStatisticViewModel(MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;
            _DataService=new DataService();

            #region Commands

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommand);

            #endregion
        }
    }
}
