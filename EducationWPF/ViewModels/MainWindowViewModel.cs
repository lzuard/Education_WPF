using EducationWPF.Infrastructure.Commands;
using EducationWPF.Models;
using EducationWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace EducationWPF.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {

        #region SelectedPageIndex:int

        private int _SelectedPageIndex = 0;

        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }

        #endregion

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

        #region Change Tab Index

        public ICommand ChangeTabIndexCommand { get; }
        private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }
        #endregion

        #endregion


        public MainWindowViewModel()
        {
            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
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
