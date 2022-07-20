using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationWPF.Services;
using EducationWPF.ViewModels.Base;

namespace EducationWPF.ViewModels
{
    internal class CountiesStatisticViewModel : ViewModel
    {
        private DataService _DataService;

        private MainWindowViewModel MainModel { get; }
        public CountiesStatisticViewModel(MainWindowViewModel MainModel)
        {
            MainModel = MainModel;
            _DataService=new DataService();
        }
    }
}
