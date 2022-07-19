﻿using System.Linq;
using System.Windows;
using EducationWPF.Services;

namespace EducationWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;

        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            base.OnStartup(e);

            var service_test = new DataService();
            var countries = service_test.GetData().ToArray();
        }
    }
}
