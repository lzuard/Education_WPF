using EducationWPF.Infrastructure.Commands;
using EducationWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using EducationWPF.Models.Decanat;
using EducationWPF.Services.Interfaces;
using DataPoint = EducationWPF.Models.DataPoint;

namespace EducationWPF.ViewModels
{
    [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
    internal class MainWindowViewModel: ViewModel
    {

        private readonly IAsyncDataService _asyncData;
        /*----------------------------------------------------------------------------------*/
        public CountriesStatisticViewModel CountriesStatistic { get; }

        public WebServerViewModel WebServer { get; }


        /*----------------------------------------------------------------------------------*/



        #region StudentFilterText : String

        private string _StudentFilterText;

        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if(!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            }
        }
        
        #endregion

        #region SelectedGroupStudents
        private void OnStudentFiltered(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }

            var filter_text = _StudentFilterText;
            if (string.IsNullOrWhiteSpace(filter_text)) return;

            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                e.Accepted = false;
                return;
            }
            if(student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if(student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Patronymic.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;


        }

        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View; 
        #endregion

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

        #region DataValue : string - Long sync operation result

        /// <summary>Long sync operation result</summary>
        private string _DataValue;

        public string DataValue
        {
            get => _DataValue;
            private set => Set(ref _DataValue, value);
        }

        #endregion


        /*----------------------------------------------------------------------------------*/

        #region Commands

        #region Close application command
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p) 
        {
            (RootObject as Window)?.Close();
            //Application.Current.Shutdown();
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

        #region Command StartProcess - Process Startup

        public ICommand StartProcessCommand { get; }


        private bool CanStartProcessCommandExecute(object p) => true;

        private void OnStartProcessCommandExecuted(object p)
        {
            new Thread(ComputeValue).Start();
        }

        private void ComputeValue()
        {
            DataValue = _asyncData.GetResult(DateTime.Now);
        }

        #endregion

        #region Command StopProcess - Process terminating

        public ICommand StopProcessCommand { get; }


        private bool CanStopProcessCommandExecute(object p) => true;

        private void OnStopProcessCommandExecuted(object p)
        {

        }

        #endregion

        #endregion

        /*----------------------------------------------------------------------------------*/

        public MainWindowViewModel(CountriesStatisticViewModel statistic, IAsyncDataService asyncData, WebServerViewModel webServer)
        {
            CountriesStatistic = statistic;
            WebServer = webServer;
            statistic.MainModel = this;
            _asyncData=asyncData;

            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            StartProcessCommand = new LambdaCommand(OnStartProcessCommandExecuted, CanStartProcessCommandExecute);
            StopProcessCommand = new LambdaCommand(OnStopProcessCommandExecuted, CanStopProcessCommandExecute);

            #endregion

            ////var data_points=new List<DataPoint>((int)(360/0.1));
            ////for (var x=0d; x <= 360; x += 0.1)
            ////{
            ////    const double to_rad = Math.PI / 180;
            ////    var y=Math.Sin(x*to_rad);
            ////    data_points.Add(new DataPoint { XValue= x, YValue = y });
            ////}
            ////TestDataPoints = data_points;
        }


        /*----------------------------------------------------------------------------------*/
    }
}
