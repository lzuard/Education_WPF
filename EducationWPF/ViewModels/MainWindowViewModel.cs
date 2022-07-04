using EducationWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationWPF.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        #region Window title
        private string _Title="Some title";

        /// <summary>Window title</summary>
        public string Title
        {
            get => _Title;
            //set
            //{
            //    if (Equals(_Title, value)) return;
            //    _Title= value;
            //    OnPropertyChanged();
            //}
            set=> Set(ref _Title, value);

        }
        #endregion

    }
}
