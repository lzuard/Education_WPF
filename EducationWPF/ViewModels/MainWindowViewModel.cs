using EducationWPF.Infrastructure.Commands;
using EducationWPF.Models;
using EducationWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EducationWPF.Models.Decanat;

namespace EducationWPF.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        /*----------------------------------------------------------------------------------*/
        public ObservableCollection<Group> Groups { get; }

        public object[] CompositeCollection { get; }

        #region SelectedCompositeValue:Object

        private object _SelectedCompositeValue;

        public object SelectedCompositeValue
        {
            get => _SelectedCompositeValue;
            set => Set(ref _SelectedCompositeValue, value);
        }

        #endregion

        #region SelectedGroup:Group

        private Group _SelectedGroup;

        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set => Set(ref _SelectedGroup, value);
        }

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

        public IEnumerable<Student> TestStudents =>
            Enumerable.Range(1,App.IsDesignMode ? 10:100_000)
            .Select(i=>new Student
            {
                Name = $"Name {i}",
                Surname=$"Surname {i}"
            });


        /*----------------------------------------------------------------------------------*/

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

        #region Create New Group Command
        public ICommand CreateNewGroupCommand { get; }

        private bool CanCreateNewGroupExecute(object p) => true;

        private void OnCreateNewGroupExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        } 
        #endregion

        #region Delete Group Command
        public ICommand DeleteGroupCommand { get; }
        private bool CanDeleteGroupExecute(object p) => p is Group group && Groups.Contains(group);

        private void OnDeleteGroupExecuted(object p)
        {
            if (!(p is Group group)) return;
            var group_index= Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
                SelectedGroup = Groups[group_index];
        } 
        #endregion

        #endregion

        /*----------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            CreateNewGroupCommand = new LambdaCommand(OnCreateNewGroupExecuted, CanCreateNewGroupExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupExecuted, CanDeleteGroupExecute);
            #endregion

            var data_points=new List<DataPoint>((int)(360/0.1));
            for (var x=0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y=Math.Sin(x*to_rad);
                data_points.Add(new DataPoint { XValue= x, YValue = y });
            }
            TestDataPoints = data_points;

            var student_index = 1;
            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name=$"Name {student_index}",
                Surname=$"Surname {student_index}",
                Patronymic=$"Patronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating=0
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)

            });
            Groups = new ObservableCollection<Group>(groups);

            var data_list = new List<object>();
            data_list.Add("Hey hey hey");
            data_list.Add(42);
            var group = Groups[1];
            data_list.Add(group);
            data_list.Add(group.Students[0]);
            CompositeCollection = data_list.ToArray();

        }

        /*----------------------------------------------------------------------------------*/
    }
}
