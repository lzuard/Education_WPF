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
        #region Window title : string
        private string _Title="Заголовок окна";

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

        #region Status : string
        private string _Status = "Готов!";

        /// <summary>Status of the window</summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);

        }
        #endregion


    }
}
