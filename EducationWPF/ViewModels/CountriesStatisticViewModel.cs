using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EducationWPF.Infrastructure.Commands;
using EducationWPF.Models;
using EducationWPF.Services;
using EducationWPF.ViewModels.Base;

namespace EducationWPF.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
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


        /// <summary>
        /// Debug constructor for design
        /// </summary>
        public CountriesStatisticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
                throw new InRowChangingEventException("lox");

            _Countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
            {
                Name = $"Country {1}",
                ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                {
                    Name = $"Province {i}",
                    Location = new Point(i, j),
                    Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                    {
                        Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                        Count = k
                    }).ToArray()
                }).ToArray()
            }).ToArray();
        }

        public CountriesStatisticViewModel (MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;
            _DataService=new DataService();

            #region Commands

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommand);

            #endregion
        }
    }
}
