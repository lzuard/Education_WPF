using EducationWPF.Infrastructure.Commands;
using EducationWPF.Models;
using EducationWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EducationWPF.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        #region TestDataPoints: IEnumerable
        private IEnumerable<DataPoint> _TestDataPoints;

        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }
        #endregion

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

        #region Commands

        #region Close application command
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p) 
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion


        public MainWindowViewModel()
        {
            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            #endregion

            var data_points=new List<DataPoint>((int)(360/0.1));
            for (var x=0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y=Math.Sin(x*to_rad);
                data_points.Add(new DataPoint { XValue= x, YValue = y });
            }
            TestDataPoints = data_points;
        }
    }
}
